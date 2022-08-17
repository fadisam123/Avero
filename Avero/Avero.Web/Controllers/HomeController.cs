using Avero.Core.Entities;
using Avero.Core.Enum;
using Avero.Infrastructure.Persistence.DBContext;
using Avero.Web.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Avero.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ApplicationDBContext context;
        private readonly IWebHostEnvironment webHostingEnvironment;
        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationDBContext context, IWebHostEnvironment webHostingEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
            this.webHostingEnvironment = webHostingEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult browseProducts(int page = 1, long? orderId = null)
        {
            var products = context.product.Include(p => p.wholesealer).OrderByDescending(p => p.created_at).ToList();
            var model = new viewProductsViewModel
            {
                products = products,
                productPerPage = 9,
                currentPage = page,
            };
            ViewBag.orderId = orderId;
            return View(model);
        }
        [HttpGet]
        public IActionResult search(String searchTxt, long? orderId = null)
        {
            int page = 1;
            var products = context.product.Where(p => p.name.Contains(searchTxt) || p.desc.Contains(searchTxt) || p.wholesealer.name.Contains(searchTxt) || p.wholesealer.Email.Contains(searchTxt) || p.wholesealer.marker_map_address.Contains(searchTxt)).Include(p => p.wholesealer).OrderByDescending(p => p.created_at).ToList();
            var model = new viewProductsViewModel
            {
                products = products,
                productPerPage = 9,
                currentPage = page,
            };
            ViewBag.orderId = orderId;
            return View("browseProducts", model);
        }

        [HttpGet]
        public async Task<IActionResult> viewProduct(long id)
        {
            var product = await context.product.Include(p => p.product_catagory).ThenInclude(pc => pc.catagory).Include(p => p.product_imgs).Include(p => p.wholesealer).ThenInclude(wh => wh.neighborhood).ThenInclude(ne => ne.city).FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> viewUser(String id)
        {
            var user = await context.Users.Include(u => u.neighborhood).ThenInclude(ne => ne.city).Include(u => u.product).ThenInclude(p => p.product_catagory).ThenInclude(p => p.catagory).Include(u => u.product).ThenInclude(p => p.product_imgs).FirstOrDefaultAsync(u => u.Id == id);
            return View(user);
        }


        [HttpGet]
        public async Task<long[]> addToCart(long productId, String userId, long orderId)
        {
            long[] result = new long[2];
            var product = await context.product.FindAsync(productId);
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                result[0] = -1;
                result[1] = -1;
                return (result);
            }
            else
            {
                if (!(await userManager.IsInRoleAsync(user, "Retailer")))
                {
                    result[0] = -2;
                    result[1] = -2;
                    return (result);
                }
            }
            Order order;
            if (orderId == 0)
            {
                order = new Order
                {
                    retailer_id = userId,
                    order_date = DateTime.Now,
                };
                context.order.Add(order);
                await context.SaveChangesAsync();
                orderId = order.Id;
            }
            else
            {
                order = await context.order.FindAsync(orderId);
            }

            var isProductAddedBefore = context.order_details.Where(od => od.order_id == orderId && od.product_id == productId).ToList();
            if (isProductAddedBefore.Count() == 0)
            {
                if ((await context.product.FindAsync(productId)).quantity_available > 0)
                {
                    var order_details = new Order_details
                    {
                        order_id = order.Id,
                        product_id = productId,
                        quantity = 1,
                        processing_state = Order_state.pending,
                    };
                    context.order_details.Add(order_details);
                    await context.SaveChangesAsync();
                }
            }

            var orderItemCount = context.order_details.Where(od => od.order_id == orderId).ToList().Count();
            result[0] = orderItemCount;
            result[1] = orderId;
            return (result);



            /*var product = await context.product.FindAsync(productId);
            List <Product> products = new List<Product>();
            products.Add(product);*/

        }
        public async Task<long[]> RemoveFromCart(long productId, String userId, long orderId)
        {
            long[] result = new long[2];
            var user = await context.Users.FindAsync(userId);
            if (user == null)
            {
                result[0] = -1;
                result[1] = -1;
                return (result);
            }
            else
            {
                if (!(await userManager.IsInRoleAsync(user, "Retailer")))
                {
                    result[0] = -2;
                    result[1] = -2;
                    return (result);
                }
            }
            var orderDetailsToRemove = context.order_details.FirstOrDefault(od => od.product_id == productId && od.order_id == orderId);
            context.order_details.Remove(orderDetailsToRemove);
            await context.SaveChangesAsync();

            var orderItemCount = context.order_details.Where(od => od.order_id == orderId).ToList().Count();
            result[0] = orderItemCount;
            result[1] = orderId;
            return (result);
        }
    }
}