using BookStoreWeb.Data;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public GenreController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Genre> genres = await dbContext.Genres.Include(genre => genre.Products).ToListAsync();
            return View(genres);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await dbContext.Genres.AddAsync(genre);
                await dbContext.SaveChangesAsync();
                TempData["successMessage"] = "Create genre successfully!";
            }
            else
            {
                TempData["errorMessage"] = "Failed to create genre!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] Genre genre)
        {
            if (ModelState.IsValid)
            {
                dbContext.Genres.Update(genre);
                await dbContext.SaveChangesAsync();
                TempData["successMessage"] = "Update genre successfully!";
            }
            else
            {
                TempData["errorMessage"] = "Failed to update genre!";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            if (ModelState.IsValid)
            {
                var existingGenre = dbContext.Genres.Include(genre => genre.Products).FirstOrDefault(genre => genre.Id == id);
                if (existingGenre != null)
                {
                    if (existingGenre.Products?.Count == 0)
                    {
                        dbContext.Genres.Remove(existingGenre);
                        await dbContext.SaveChangesAsync();
                        TempData["successMessage"] = "Delete genre successfully!";
                    }
                    else
                    {
                        TempData["errorMessage"] = "Can't delete genre that has books!";
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Genre does not exist!";
                }
            }
            else
            {
                TempData["errorMessage"] = "Failed to update genre!";
            }
            return RedirectToAction("Index");
        }
    }
}
