namespace CakeShop.Web.ViewModels.Recipes.ViewModels
{
    using System;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class SidebarRecipeViewModel : RecipeBaseViewModel, IMapFrom<Recipe>
    {
        //format
        public DateTime CreatedOn { get; set; }
    }
}
