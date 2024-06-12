using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.ViewComponents
{
    public class ToggleThemeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (HttpContext.Session.GetString(ThemeSession.SessionKey) == null)
            {
                return View("Default", ThemeSession.LightMode);
            }

            return View("Default", HttpContext.Session.GetString(ThemeSession.SessionKey));
        }
    }
}
