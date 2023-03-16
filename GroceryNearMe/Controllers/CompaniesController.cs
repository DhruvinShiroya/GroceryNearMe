using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroceryNearMe.Data;
using GroceryNearMe.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace GroceryNearMe.Controllers
{
    [Authorize(Roles = "Administrator")]
    
    public class CompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Companies
        public async Task<IActionResult> Index()
        {
              return View(await _context.Companies.ToListAsync());
        }

        // GET: Companies/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Companies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Image")] Company company, IFormFile Image,string? CurrentPhoto)
        {
            
            if (ModelState.IsValid)
            {
                if(Image != null)
                {
                    company.Image = UploadPhoto(Image);
                }
                else
                {
                    company.Image = CurrentPhoto;
                }
                _context.Add(company);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        // GET: Companies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] Company company, IFormFile? Image, string? currentPhoto)
        {
            
            
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    // add file name to the database
                    DeleteImage(currentPhoto);
                    company.Image = UploadPhoto(Image);
                }
                else
                {
                    // keep the original photo
                    company.Image = currentPhoto;
                }

                try
                {
                    _context.Update(company);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            return View(company);
        }

        // GET: Companies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Companies == null)
            {
                return NotFound();
            }

            var company = await _context.Companies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Companies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company != null)
            {
                DeleteImage(company.Image);
                _context.Companies.Remove(company);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyExists(int id)
        {
          return _context.Companies.Any(e => e.Id == id);
        }

        private static string UploadPhoto(IFormFile image)
        {
            // create unique file name to store in the directory
            var fileName = Guid.NewGuid().ToString() + "-" + image.FileName;
            // add prefix to your path so it is saved in the directory you want
            var uploadFilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\company\\" + fileName;
            // copy photo to the uploadFilePath
            using (var stream = new FileStream(uploadFilePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return fileName;
        }

        private static void DeleteImage(string photo)
        {
            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\img\\company\\" + photo;

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
