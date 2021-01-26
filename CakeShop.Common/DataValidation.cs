namespace CakeShop.Common
{
    public static class DataValidation
    {
        // User
        public const int UserFirstNameMinLenght = 2;

        public const int UserFirstNameMaxLenght = 50;

        public const int UserLastNameMinLenght = 2;

        public const int UserLastNameMaxLenght = 50;

        public const int UserAddressMinLenght = 10;

        public const int UserAddressMaxLenght = 200;

        public const int UserPasswordMinLenght = 6;

        public const int UserPasswordMaxLenght = 100;

        // Category
        public const int CategotyNameMinLenght = 2;

        public const int CategotyNameMaxLenght = 50;

        public const int CategotyDescriptionMinLenght = 10;

        public const int CategotyDescriptionMaxLenght = 500;

        // Comment
        public const int CommentContentMaxLenght = 1000;

        // Dessert
        public const int DessertNameMinLenght = 2;

        public const int DessertNameMaxLenght = 200;

        public const int DessertDescriptionMinLenght = 10;

        public const int DessertDescriptionMaxLenght = 2000;

        public const string DessertMinPrice = "1";

        public const string DessertMaxPrice = "2000";

        // Ingredient
        public const int IngredientNameMinLenght = 3;

        public const int IngredientNameMaxLenght = 150;

        // Order
        public const int OrderDeliveryAddressMinLenght = 10;

        public const int OrderDeliveryAddressMaxLenght = 200;

        public const string MinQuantity = "1";

        public const string MaxQuantity = "120";

        public const int OrderNotesMaxLenght = 500;

        // Recipe
        public const int RecipeTitleMinLenght = 3;

        public const int RecipeTitleMaxLenght = 200;

        public const int RecipeContentMinLenght = 10;

        public const int RecipeContentMaxLenght = 3000;

        public const string MinPortionsValue = "1";

        public const string MaxPortionsValue = "100";

        public const string MinPreparationTimeValue = "0";

        public const string MaxPreparationTimeValue = "5000";

        public const string MinCookingTimeValue = "0";

        public const string MaxCookingTimeValue = "5000";

        // Review
        public const int ReviewContentMaxLenght = 1000;

        // Tag
        public const int TagNameMinLenght = 3;

        public const int TagNameMaxLenght = 200;

        // RecipeIngredient
        public const int RepiceIngredientQuantityMaxLenght = 100;
    }
}
