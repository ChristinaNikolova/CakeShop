namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class SidebarRecipeViewModel : IMapFrom<Recipe>
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Picture { get; set; }

        //format
        public DateTime CreatedOn { get; set; }
    }
}
