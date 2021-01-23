namespace CakeShop.Web.ViewModels.Administration.Desserts.ViewModels
{
    using System.Collections.Generic;

    public class AllDessertsAdminViewModel
    {
        public IEnumerable<DessertAdminViewModel> Desserts { get; set; }
    }
}
