namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using System.Collections.Generic;

    public class AllDessertsByCategoryViewModel
    {
        public IEnumerable<DessertViewModel> Desserts { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage + 1;
    }
}
