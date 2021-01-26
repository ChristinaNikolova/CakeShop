namespace CakeShop.Web.ViewModels.Administration.Categories.ViewModels
{
    using System.Collections.Generic;

    public class AllCategoriesAdminViewModel
    {
        public IEnumerable<CategoryAdminViewModel> Categories { get; set; }
    }
}
