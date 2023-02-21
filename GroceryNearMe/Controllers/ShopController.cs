using GroceryNearMe.Data;
using Microsoft.AspNetCore.Mvc;

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
    }
}
