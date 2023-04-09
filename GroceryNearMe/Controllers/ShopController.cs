using GroceryNearMe.Data;
using GroceryNearMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Build.ObjectModelRemoting;

namespace GroceryNearMe.Controllers
{
    
    public class ShopController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShopController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var result = _context.Categories.OrderBy(c => c.Name).ToList();

                        

            return View(result);
        }


        public IActionResult Category(int id)
        {
            var productByCategory = _context.Categories
                .Include(c => c.Products)
                .FirstOrDefault(c => c.Id == id);

            if (productByCategory == null)
            {
                return NotFound();
            }
            return View(productByCategory);
        }

        // add product to the cartItem.
        [HttpPost]
        public  async Task<IActionResult> AddToCart([FromForm] int productId, [FromForm]  int quantity)
        {
            //get the customer id

            var customerId = getUserId();

            // Query the db to get the price how much  are they paying


            Console.WriteLine("this part is working");
            var product = await _context.Products.FindAsync(productId);
            var price = product.Price;

            
            // Create a CartItem
            var cartItem = new CartItem()
            {
                ProductId = productId,
                CustomerId = customerId,
                Price = (decimal)price,
                Quantity = quantity,
                DateCreated = DateTime.UtcNow

            };

            // Save cart 

            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            // redirect to cart view

            if(User.Identity.IsAuthenticated)
            {
                return Redirect("Cart");
            }
            return Redirect("Login");

        }

        public IActionResult Cart()
        {
            var customerId = getUserId();

            var cartItem = _context.CartItems
                .Include(c => c.Product)
                .Where(c => c.CustomerId == customerId)
                .OrderByDescending(c => c.DateCreated)
                .ToList();

            return View(cartItem);
        }

        public String getUserId()
        {
            string customerId = HttpContext.Session.GetString("CustomerId") ?? "";

            if (String.IsNullOrEmpty(customerId))
            {
                // see if user has auntentication id
                if (User?.Identity?.IsAuthenticated ?? false)
                {
                    customerId = User.Identity.Name;
                }
                else
                {
                    //gemerate id for customer
                    customerId = Guid.NewGuid().ToString();
                }
                HttpContext.Session.SetString("CustomerId", customerId);
            }
            
            return customerId ;
        }
    }
} 
