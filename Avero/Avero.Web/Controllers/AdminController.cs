using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Avero.Core.Entities;
using Avero.Infrastructure.Persistence.DBContext;
using Microsoft.AspNetCore.Identity;
using Avero.Web.ViewModels;
using Avero.Web.ViewModels.Admin;
using Avero.Core.Enum;

namespace Avero.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDBContext context;
        private readonly IWebHostEnvironment webHostingEnvironment;

        public AdminController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDBContext context, IWebHostEnvironment webHostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.webHostingEnvironment = webHostingEnvironment;
        }

        // GET: Admin
        public async Task<IActionResult> Profile(String id)
        {
            ViewBag.active = "Profile";

            List<City> cityes = new List<City>();
            cityes = (from c in context.city select c).ToList();
            ViewBag.cityes = cityes;

            var user = await userManager.FindByIdAsync(id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {id} cannot be found";
                return View("NotFound");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new RegisterViewModel
            {
                id = user.Id,
                Email = user.Email,
                name = user.name,
                street_name = user.street_name,
                neighborhood = user.neighborhood_id,
                is_wholesealer = await userManager.IsInRoleAsync(user, "WholeSealer"),
                /*Photo*/
                latitude = user.latitude,
                longitude = user.longitude,
                marker_map_address = user.marker_map_address,
                Phone = user.PhoneNumber,
                Password = user.PhoneNumber,
            };

            ViewBag.city_id = context.neighborhood.Find(user.neighborhood_id).city_id;

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(RegisterViewModel model)
        {
            ViewBag.active = "Profile";
            var user = await userManager.FindByIdAsync(model.id);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {model.id} cannot be found";
                return View("NotFound");
            }
            else
            {
                // delete old image if user upload new image
                string? uniqueFileName = null;
                if (model.Photo != null && model.Photo.FileName != "Default.jpg")
                {
                    if (user.img_name != null)
                    {
                        string oldImage = Path.Combine(webHostingEnvironment.WebRootPath, "img", "users", user.img_name);
                        if ((System.IO.File.Exists(oldImage)))
                        {
                            System.IO.File.Delete(oldImage);
                        }
                    }

                    string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "img", "users");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(fileStream);
                    }
                    user.img_name = uniqueFileName;
                }


                user.name = model.name;
                user.Email = user.Email;
                user.street_name = model.street_name;
                user.PhoneNumber = model.Phone;
                user.neighborhood_id = model.neighborhood;
                user.latitude = model.latitude;
                user.longitude = model.longitude;
                user.marker_map_address = model.marker_map_address;

                var result = await userManager.UpdateAsync(user);

                List<City> cityes = new List<City>();
                cityes = (from c in context.city select c).ToList();
                ViewBag.cityes = cityes;

                if (result.Succeeded)
                {
                    model.Email = user.Email;
                    model.latitude = user.latitude;
                    model.longitude = user.longitude;
                    model.marker_map_address = user.marker_map_address;
                    model.neighborhood = user.neighborhood_id;
                    ViewBag.city_id = context.neighborhood.Find(user.neighborhood_id).city_id;
                    TempData["confirm"] = "true";
                    return View(model);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> viewProducts(String id, int page = 1)
        {
            ViewBag.active = "viewProducts";
            var model = new viewProductsViewModel
            {
                productPerPage = 9,
                products = context.product.Include(p => p.product_imgs).Where(p => p.wholesealer_id == id).OrderByDescending(p => p.created_at),
                currentPage = page,
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.active = "AddProduct";
            var model = new addProductViewModel();
            model.catagories = context.catagory.Select(c => new CatagoriesViewModel { catagory_id = c.Id, catagory_name = c.name, IsSelected = false }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(addProductViewModel model)
        {
            ViewBag.active = "AddProduct";

            if (
                (ModelState.IsValid && (model.make_discount && (model.offer_price > 0) && (model.offer_price_start_date != null) && (model.offer_price_end_date != null)))
                || (ModelState.IsValid && !model.make_discount)
                )
            {
                // processing product
                var product = new Product
                {
                    name = model.name,
                    desc = model.desc,
                    quantity_available = model.quantity_available,
                    created_at = DateTime.Now,
                    price_per_unit = model.price_per_unit,
                    offer_price = model.offer_price,
                    offer_price_start_date = model.offer_price_start_date,
                    offer_price_end_date = model.offer_price_end_date,
                    rating = Rating.zero,
                    rated_people_count = 0,
                    wholesealer_id = model.wholesealer_id,
                };

                await context.product.AddAsync(product);
                await context.SaveChangesAsync();

                // processing catagories
                var productCatagories = new Product_catagory[model.catagories.Count()];
                for (int i = 0; i < model.catagories.Count(); i++)
                {
                    if (model.catagories[i].IsSelected)
                    {
                        productCatagories[i] = new Product_catagory { product_id = product.Id, catagory_id = model.catagories[i].catagory_id };
                        context.product_catagory.Add(productCatagories[i]);
                    }

                }
                await context.SaveChangesAsync();

                // processing images
                if ((model?.imgs?.Count() ?? 0) > 0)
                {
                    List<Product_imgs> pimgs = new List<Product_imgs>();
                    List<string> uniqueFilesNames = new List<string>();
                    string uploadsFolder = Path.Combine(webHostingEnvironment.WebRootPath, "img", "products");
                    for (int i = 0; i < model?.imgs?.Count(); i++)
                    {
                        uniqueFilesNames.Add(Guid.NewGuid().ToString() + "_" + model.imgs[i].FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFilesNames[i]);
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            model.imgs[i].CopyTo(fileStream);
                        }
                        var pimg = new Product_imgs { img_name = uniqueFilesNames[i], product_id = product.Id };
                        pimgs.Add(pimg);
                    }
                    await context.product_imgs.AddRangeAsync(pimgs);
                    await context.SaveChangesAsync();
                }

                TempData["add"] = "add";
                return RedirectToAction("viewProducts", new { id = product.wholesealer_id });
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> editProduct(long id)
        {
            var product = await context.product.Include(p => p.product_imgs).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                ViewBag.ErrorMessage = $"The Product ID {id} is invalid";
                return View("NotFound");
            }
            var model = new addProductViewModel
            {
                name = product.name,
                desc = product.desc,
                quantity_available = product.quantity_available,
                created_at = product.created_at,
                price_per_unit = product.price_per_unit,
                offer_price = product.offer_price,
                offer_price_start_date = product.offer_price_start_date,
                offer_price_end_date = product.offer_price_end_date,
                wholesealer_id = product.wholesealer_id,
                product = product,
                catagories = context.catagory.Select(c => new CatagoriesViewModel { catagory_id = c.Id, catagory_name = c.name, IsSelected = context.product_catagory.Where(pc => pc.catagory_id == c.Id && pc.product_id == id).FirstOrDefault() != null ? true : false }).ToList(),
            };
            ViewBag.productId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> editProduct(addProductViewModel model, long id)
        {
            if (
                (ModelState.IsValid && (model.make_discount && (model.offer_price > 0) && (model.offer_price_start_date != null) && (model.offer_price_end_date != null)))
                || (ModelState.IsValid && !model.make_discount)
                )
            {
                var product = await context.product.FindAsync(id);
                if (product == null)
                {
                    ViewBag.ErrorMessage = $"The Product ID {id} is invalid";
                    return View("NotFound");
                }

                product.name = model.name;
                product.desc = model.desc;
                product.quantity_available = model.quantity_available;
                product.price_per_unit = model.price_per_unit;
                product.offer_price = model.offer_price;
                product.offer_price_start_date = model.offer_price_start_date;
                product.offer_price_end_date = model.offer_price_end_date;

                await context.SaveChangesAsync();

                // remove all old catagories
                var pcs = context.product_catagory.Where(pc => pc.product_id == id);
                context.product_catagory.RemoveRange(pcs);
                await context.SaveChangesAsync();

                // add all checked catagories
                var productCatagories = new Product_catagory[model.catagories.Count()];
                for (int i = 0; i < model.catagories.Count(); i++)
                {
                    if (model.catagories[i].IsSelected)
                    {
                        productCatagories[i] = new Product_catagory { product_id = product.Id, catagory_id = model.catagories[i].catagory_id };
                        context.product_catagory.Add(productCatagories[i]);
                    }

                }
                await context.SaveChangesAsync();

                TempData["edit"] = "edit";
                return RedirectToAction("viewProducts", new { id = product.wholesealer_id });
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> deleteProduct(long id, int currentPage)
        {
            var productToDelete = await context.product.FindAsync(id);
            var productImgs = context.product_imgs.Where(pi => pi.product_id == id);
            var productCatagories = context.product_catagory.Where(pc => pc.product_id == id);
            var userId = productToDelete.wholesealer_id;

            // delete product images in WWWroot folder
            var imegesList = productImgs.ToList();
            for (int i = 0; i < productImgs.Count(); i++)
            {
                var imageToDelete = Path.Combine(webHostingEnvironment.WebRootPath, "img", "products", imegesList.ElementAt(i).img_name);
                if ((System.IO.File.Exists(imageToDelete)))
                {
                    System.IO.File.Delete(imageToDelete);
                }
            }

            context.product.Remove(productToDelete);
            context.product_imgs.RemoveRange(productImgs);
            context.product_catagory.RemoveRange(productCatagories);
            await context.SaveChangesAsync();
            TempData["remove"] = "remove";
            return RedirectToAction("viewProducts", new
            {
                id = userId,
                page = currentPage
            });
        }

        [HttpGet]
        public async Task<IActionResult> Cart(String id)
        {
            ViewBag.active = "Cart";

            var user = await context.Users.Include(u => u.order).ThenInclude(o => o.order_details).ThenInclude(od => od.product).ThenInclude(od => od.product_imgs).Include(u => u.order).ThenInclude(o => o.order_details).ThenInclude(od => od.product).ThenInclude(p => p.product_catagory).ThenInclude(pc => pc.catagory).FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> clearCart(String id, long orderId)
        {
            ViewBag.active = "Cart";

            var orderDetails = context.order_details.Where(od => od.order_id == orderId);
            context.RemoveRange(orderDetails);
            await context.SaveChangesAsync();

            var user = await context.Users.Include(u => u.order).ThenInclude(o => o.order_details).ThenInclude(od => od.product).ThenInclude(od => od.product_imgs).Include(u => u.order).ThenInclude(o => o.order_details).ThenInclude(od => od.product).ThenInclude(p => p.product_catagory).ThenInclude(pc => pc.catagory).FirstOrDefaultAsync(u => u.Id == id);
            return RedirectToAction("Cart", user);
        }

        public async Task<IActionResult> checkOut(String id, long orderId)
        {
            
            var oldOrder = await context.order.Include(o => o.order_details).FirstOrDefaultAsync(o => o.Id == orderId);
            if (oldOrder.order_details.Any())
            {
                oldOrder.order_date = DateTime.Now;
                await context.SaveChangesAsync();

                var newOrder = new Order
                {
                    retailer_id = id,
                    order_date = DateTime.Now,
                };
                context.order.Add(newOrder);
                await context.SaveChangesAsync();

                return RedirectToAction("orders", new { id });
            }

            else 
            {
                return RedirectToAction("Cart", new { id });
            }
            
        }

        [HttpGet]
        public IActionResult orders()
        {
            ViewBag.active = "orders";
            return View();
        }


    }
}
