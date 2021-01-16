namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using System.Linq;

    using AutoMapper;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertDetailsViewModel : DessertViewModel, IMapFrom<Dessert>, IHaveCustomMappings
    {
        public string Description { get; set; }

        public bool IsFavourite { get; set; }

        public string[] TagNames { get; set; }

        public string[] IngredientNames { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Dessert, DessertDetailsViewModel>()
                .ForMember(
                    d => d.TagNames,
                    opt => opt.MapFrom(x => x.DessertTags.Select(y => y.Tag.Name).OrderBy(y => y)))
                .ForMember(
                d => d.IngredientNames,
                opt => opt.MapFrom(x => x.DessertIngredients.Select(y => y.Ingredient.Name).OrderBy(y => y)));
        }
    }
}
