using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

#nullable disable

namespace BookStoreWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword = "", string? genre = null, int page = 1, int perPage = 8)
        {
            IEnumerable<Product> products = await _unitOfWork.Product.GetAllAsync(product => 
                product.Title.ToLower().Contains(keyword.ToLower()) 
                    && (genre == null || genre == "all" || genre.ToLower().Equals(product.GenreId.ToString().ToLower())), 
                includeProperties: "Genre", page: page, perPage: perPage);

            int total = (await _unitOfWork.Product.GetAllAsync(product =>
                product.Title.ToLower().Contains(keyword.ToLower()) 
                && (genre == null || genre == "all" || genre.ToLower().Equals(product.GenreId.ToString().ToLower())))).Count();

            IEnumerable<Genre> genres = await _unitOfWork.Genre.GetAllAsync();

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
            var product = await _unitOfWork.Product.GetAsync(product => product.Id == id, includeProperties: "Genre");

            if (product == null)
            {
                TempData["errorMessage"] = "Product not found!";
                return RedirectToAction("Index");
            }

            ShoppingCart shoppingCart = new ShoppingCart()
            {
                ProductId = product.Id,
                Product = product,
            };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Detail(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity) User.Identity;
            var userId = claimsIdentity!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = await _unitOfWork.ShoppingCart.GetAsync(cart =>
                cart.ApplicationUserId == userId && cart.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null) 
            {
                cartFromDb.Quantity += shoppingCart.Quantity;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            } 
            else
            {
                await _unitOfWork.ShoppingCart.AddAsync(shoppingCart);
            }
            await _unitOfWork.SaveChangesAsync();

            TempData["successMessage"] = "Add to cart successfully";

            shoppingCart.Product = await _unitOfWork.Product.GetAsync(product => 
                product.Id == shoppingCart.ProductId, includeProperties: "Genre");

            return View(shoppingCart);
        }
    }
}
