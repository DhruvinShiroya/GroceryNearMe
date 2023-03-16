using GroceryNearMe.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            var productByCategory = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);

            if (productByCategory == null)
            {
                return NotFound();
            }
            return View(productByCategory);
        }    
    }
} 
