using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class GenreController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public GenreController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Genre> genres = await unitOfWork.Genre.GetAllAsync(includeProperties: "Products");
            return View(genres);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await unitOfWork.Genre.AddAsync(genre);
                await unitOfWork.SaveChangesAsync();
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
                unitOfWork.Genre.Update(genre);
                await unitOfWork.SaveChangesAsync();
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
                var existingGenre = await unitOfWork.Genre.GetAsync(genre => genre.Id == id, includeProperties: "Products");
                if (existingGenre != null)
                {
                    if (existingGenre.Products?.Count == 0)
                    {
                        unitOfWork.Genre.Remove(existingGenre);
                        await unitOfWork.SaveChangesAsync();
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
