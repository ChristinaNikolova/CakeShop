namespace CakeShop.Web.ViewModels.Desserts.ViewModels
{
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class DessertViewModel : IMapFrom<Dessert>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        //public string Description { get; set; }

        public string CategoryName { get; set; }

        //public virtual ICollection<DessertTag> DessertTags { get; set; }

        //public virtual ICollection<DessertIngredient> DessertIngredients { get; set; }

        //public virtual ICollection<DessertLike> DessertLikes { get; set; }

        //public virtual ICollection<Review> Reviews { get; set; }
    }
}
