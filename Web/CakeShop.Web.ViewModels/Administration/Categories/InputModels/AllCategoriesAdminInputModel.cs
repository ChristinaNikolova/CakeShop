namespace CakeShop.Web.ViewModels.Administration.Categories.InputModels
{
    using System.Collections.Generic;

    using CakeShop.Web.ViewModels.Administration.Categories.ViewModels;

    public class AllCategoriesAdminInputModel
    {
        //change to ViewModel
        public IEnumerable<CategoryAdminViewModel> Categories { get; set; }
    }
}
