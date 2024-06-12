using BookStore.Utility.StaticDetails;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreWeb.Controllers
{
    public class SettingsController : Controller
    {
        public SettingsController()
        {
            
        }

        #region API CALLS

        [HttpPost]
        public IActionResult ToggleDarkMode([FromBody] bool darkMode)
        {
            HttpContext.Session.SetString(ThemeSession.SessionKey, darkMode ? ThemeSession.DarkMode : ThemeSession.LightMode);
            return Ok(new { success = true });
        }

        #endregion
    }
}
