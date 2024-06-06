﻿using Microsoft.AspNetCore.Mvc.Rendering;

#nullable disable

namespace BookStoreWeb.Areas.Admin.Views
{
    public static class ManageNavPages
    {
        public static string Dashboard => "Dashboard";
        public static string Order => "Order";
        public static string Product => "Product";
        public static string Genre => "Genre";
        public static string Customer => "Customer";

        public static string DashboardNavClass(ViewContext viewContext) => PageNavClass(viewContext, Dashboard);
        public static string OrderNavClass(ViewContext viewContext) => PageNavClass(viewContext, Order);
        public static string ProductNavClass(ViewContext viewContext) => PageNavClass(viewContext, Product);
        public static string GenreNavClass(ViewContext viewContext) => PageNavClass(viewContext, Genre);
        public static string CustomerNavClass(ViewContext viewContext) => PageNavClass(viewContext, Customer);

        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : "link-body-emphasis";
        }
    }
}