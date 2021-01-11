namespace CakeShop.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CakeShop";

        public const string AdministratorRoleName = "Administrator";

        // seed data
        public const string TagSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Tags.json";

        public const string IngredientSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Ingredients.json";

        public const string CategorySeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Categories.json";

        public const string DessertSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Desserts.json";

        // errors
        public const string ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ErrorMessagePasswords = "The password and confirmation password do not match.";
    }
}
