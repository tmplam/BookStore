using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUnitOfWork unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", string? genre = null, int page = 1, int perPage = 8)
        {
            IEnumerable<Product> products = await unitOfWork.Product.GetAllAsync(product => 
                product.Title.ToLower().Contains(keyword.ToLower()) 
                    && (genre == null || genre == "all" || genre.ToLower().Equals(product.GenreId.ToString().ToLower())), 
                includeProperties: "Genre", page: page, perPage: perPage);

            int total = (await unitOfWork.Product.GetAllAsync(product =>
                product.Title.ToLower().Contains(keyword.ToLower()) 
                && (genre == null || genre == "all" || genre.ToLower().Equals(product.GenreId.ToString().ToLower())))).Count();

            IEnumerable<Genre> genres = await unitOfWork.Genre.GetAllAsync();

            HomeVM model = new HomeVM()
            {
                Page = page,
                PerPage = perPage,
                TotalPage = total / perPage + (total % perPage == 0 ? 0 : 1),
                Keyword = keyword,
                Genre = genre,
                ProductList = products,
                GenreList = genres.Select(genre => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = genre.Id.ToString(),
                    Text = genre.Name,
                })
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id)
        {
            var product = await unitOfWork.Product.GetAsync(product => product.Id == id, includeProperties: "Genre");

            if (product == null)
            {
                TempData["errorMessage"] = "Product not found!";
                return RedirectToAction("Index");
            }
            return View(product);
        }
    }
}
