using BookStore.DataAccess.Repository.IRepository;
using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStoreWeb.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity?)User.Identity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(ShoppingCartSession.SessionKey) == null)
                {
                    var cartCount = (await _unitOfWork.ShoppingCart.GetAllAsync(cart =>
                        cart.ApplicationUserId == claim.Value)).Count();
                    HttpContext.Session.SetInt32(ShoppingCartSession.SessionKey, cartCount);
                }
                return View(HttpContext.Session.GetInt32(ShoppingCartSession.SessionKey));
            }
            else
            {
                HttpContext.Session.Remove(ShoppingCartSession.SessionKey);
                return View(0);
            }
        }
    }
}
