namespace CakeShop.Data.Seeding.Dtos
{
    public class DessertDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string[] DessertTags { get; set; }

        public string[] DessertIngredients { get; set; }
    }
}
