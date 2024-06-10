using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace BookStoreWeb.Views
{
    public static class ManageNavPages
    {
        public static string Home => "Home";
        public static string Order => "Order";
        public static string Cart => "Cart";

        public static string HomeNavClass(ViewContext viewContext) => PageNavClass(viewContext, Home);
        public static string OrderNavClass(ViewContext viewContext) => PageNavClass(viewContext, Order);
        public static string CartNavClass(ViewContext viewContext) => PageNavClass(viewContext, Cart);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "link-secondary" : "link-body-emphasis";
        }
    }
}
