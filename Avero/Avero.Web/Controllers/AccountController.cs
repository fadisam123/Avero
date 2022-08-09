using Avero.Core.Entities;
using Avero.Core.Enum;
using Avero.Infrastructure.Persistence.DBContext;
using Avero.Web.ViewModels;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace Avero.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDBContext context;
        private readonly IWebHostEnvironment webHostingEnvironment;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDBContext context, IWebHostEnvironment webHostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.webHostingEnvironment = webHostingEnvironment;
        }

        [HttpGet]
        [Route("/Account/getNeighborhood/{city_id}")]
        public Neighborhood[] getNeighborhood(int city_id)
        {
            List<Neighborhood> neighbors = new List<Neighborhood>();
            neighbors = (from n in context.neighborhood where n.city_id == city_id select n).ToList();
            Neighborhood[] result = neighbors.ToArray();
            return result;
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            List<City> cityes = new List<City>();
            cityes = (from c in context.city select c).ToList();
            ViewBag.cityes = cityes;

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                string? uniqueFileName = null;
                if (model.Photo != null)
                {
                    string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "img", "users");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                }
                var user = new User
                {
                    name = model.name,
                    street_name = model.street_name,
                    registered_at = DateTime.Now,
                    neighborhood_id = model.neighborhood,
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.Phone,
                    last_login = DateTime.Now,
                    rating = Rating.zero,
                    rated_people_count = 0,
                    latitude = model.latitude,
                    longitude = model.longitude,
                    marker_map_address = model.marker_map_address,
                    img_name = uniqueFileName
                };

                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (model.is_wholesealer)
                        userManager.AddToRoleAsync(user, "WholeSealer").Wait();
                    else
                        userManager.AddToRoleAsync(user, "Retailer").Wait();

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                            new { userId = user.Id, token = token }, Request.Scheme);

                    // create email message
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("Avero Confirmation Message", "Avero-manager@outlook.com"));
                    email.To.Add(MailboxAddress.Parse(user.UserName));
                    email.Subject = "Test Email Subject";
                    email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Click Here To <a href='" + confirmationLink + "'>Confirm</a></h1>" };

                    // send email
                    using var smtp = new SmtpClient();
                    //smtp.Connect([host], [port], [true|false]);
                    smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    //smtp.Authenticate("[USERNAME]", "[PASSWORD]");
                    await smtp.AuthenticateAsync("Avero-manager@outlook.com", "avero123");
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                    smtp.Dispose();


                    ViewBag.ErrorTitle = "Registration successful";
                    ViewBag.ErrorMessage = "Before you can Login, please confirm your email, by clicking on the confirmation link we have emailed you";
                    ViewBag.showHomeLink = "true";
                    return View("Error");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            List<City> cityes = new List<City>();
            cityes = (from c in context.city select c).ToList();
            ViewBag.cityes = cityes;

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("index", "home");
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }

            var result = await userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                await signInManager.SignInAsync(user, true);
                TempData["confirm"] = "true";
                return RedirectToAction("index", "home");
            }

            ViewBag.ErrorTitle = "Email cannot be confirmed";
            return View("Error");
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl)
        {
            LoginViewModel model = new LoginViewModel
            {
                ReturnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed &&
                                    (await userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ViewBag.ErrorTitle = "Email not confirmed yet";
                    ViewBag.showHomeLink = "true";
                    {
                        ViewBag.showReConfirmLink = "true";
                        ViewBag.userID = user.Id.ToString();
                    }
                    return View("Error");
                }

                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password,
                                        model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }

                if (result.IsLockedOut)
                {
                    return View("AccountLocked");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> ResendConfirmationEmail(string userId)
        {
            if (userId != null)
            {
                var user = await userManager.FindByIdAsync(userId);
                var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Account",
                                        new { userId = user.Id, token = token }, Request.Scheme);

                // create email message
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Avero Confirmation Message", "Avero-manager@outlook.com"));
                email.To.Add(MailboxAddress.Parse(user.UserName));
                email.Subject = "Test Email Subject";
                email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Click Here To <a href='" + confirmationLink + "'>Confirm</a></h1>" };

                // send email
                using var smtp = new SmtpClient();
                //smtp.Connect([host], [port], [true|false]);
                smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                //smtp.Authenticate("[USERNAME]", "[PASSWORD]");
                await smtp.AuthenticateAsync("Avero-manager@outlook.com", "avero123");
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
                smtp.Dispose();

                ViewBag.ErrorTitle = "Email Resended";
                ViewBag.ErrorMessage = "pleas check your Email";
                ViewBag.showHomeLink = "true";
                return View("Error");
            }

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null && await userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                            new { email = model.Email, token = token }, Request.Scheme);


                    // create email message
                    var email = new MimeMessage();
                    email.From.Add(new MailboxAddress("Avero Password Reset", "Avero-manager@outlook.com"));
                    email.To.Add(MailboxAddress.Parse(user.UserName));
                    email.Subject = "Test Email Subject";
                    email.Body = new TextPart(TextFormat.Html) { Text = "<h1>Click Here To <a href='" + passwordResetLink + "'>Reset</a> Your Password</h1>" };

                    // send email
                    using var smtp = new SmtpClient();
                    //smtp.Connect([host], [port], [true|false]);
                    smtp.Connect("smtp.office365.com", 587, SecureSocketOptions.StartTls);
                    //smtp.Authenticate("[USERNAME]", "[PASSWORD]");
                    await smtp.AuthenticateAsync("Avero-manager@outlook.com", "avero123");
                    await smtp.SendAsync(email);
                    smtp.Disconnect(true);
                    smtp.Dispose();


                    ViewBag.ErrorTitle = "Forgot Password";
                    ViewBag.ErrorMessage = "If you have an account with us, we have sent an email with the instructions to reset your password.";
                    ViewBag.showHomeLink = "true";
                    return View("Error");
                }

                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid reset URL");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        if (await userManager.IsLockedOutAsync(user))
                        {
                            await userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow);
                        }
                        ViewBag.ErrorTitle = "Your password is reset";
                        ViewBag.ErrorMessage = "Please Go to Login Again";
                        ViewBag.showLoginLink = "true";
                        return View("Error");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }

                return View("Error");
            }

            return View(model);
        }

    }
}
