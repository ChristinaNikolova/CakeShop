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

        // messages SendGrid
        public const string CakeShopEmail = "christina.b.nikolova@gmail.com";

        public const string ConfirmProfileTitleMessage = "Registration Confirmation";

        public const string ConfirmProfileMessage = "<p>{0}, thank you for your registration at <strong>CakeShop</strong>! Please, click <a href={1}>here</a> to confirm your email.</p>";

        public const string ForgotPasswordTitleMessage = "Reset Password";

        public const string ForgotPasswordMessage = "Please reset your password by <a href='{0}'>clicking here</a>.";

        // pagination
        public const int DessertsPerPage = 8;
    }
}
