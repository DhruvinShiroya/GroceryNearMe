using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroceryNearMe.Data;
using GroceryNearMe.Models;

namespace GroceryNearMe.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image")] Category category, IFormFile? Image, string? CurrentPhoto)
        {
            
            if (ModelState.IsValid)
            {

                if (Image != null)
                {
                    // add file name to the database
                    category.Image = UploadPhoto(Image);
                }
                else
                {
                    // keep the original photo
                    category.Image = CurrentPhoto;
                }
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Category category, IFormFile? Image,string? CurrentPhoto)
        {
            
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                if (Image != null)
                {
                    // add file name to the database
                    DeleteImage(CurrentPhoto);
                    category.Image = UploadPhoto(Image);
                }
                else
                {
                    // keep the original photo
                    category.Image = CurrentPhoto;
                }

                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                DeleteImage(category.Image);
                _context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.Id == id);
        }

        private static string UploadPhoto(IFormFile image)
        {
            // create unique file name to store in the directory
            var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
            // add prefix to your path so it is saved in the directory you want
            var uploadFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\category\\" + fileName;
            // copy photo to the uploadFilePath
            using (var stream = new FileStream(uploadFilePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return fileName;
        }
        private static void DeleteImage(string photo)
        {
            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\category\\" + photo;

            try
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
