using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Models.ViewModels
{
    public class HomeVM
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPage { get; set; }
        public string Keyword { get; set; }
        public string? Genre { get; set; }

        public IEnumerable<Product> ProductList { get; set; }
        public IEnumerable<SelectListItem> GenreList { get; set; }
    }
}
