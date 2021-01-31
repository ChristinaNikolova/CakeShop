namespace CakeShop.Web.ViewModels.Common.ViewModels
{
    public class PaginationViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }

        public int PreviousPage
            => this.CurrentPage - 1;

        public int NextPage
            => this.CurrentPage + 1;
    }
}
