namespace CakeShop.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CakeShop";

        public const string AdministratorRoleName = "Administrator";

        public const string AdminEmail = "admin@cakeshop.com";

        // seed data
        public const string TagSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Tags.json";

        public const string IngredientSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Ingredients.json";

        public const string CategorySeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Categories.json";

        public const string DessertSeederPath = @"../../Data/CakeShop.Data/Seeding/Data/Desserts.json";

        // errors
        public const string ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.";

        public const string ErrorMessagePasswords = "The password and confirmation password do not match.";

        public const string ErrorDessertQuantity = "Invalid quantity! Quantity shoud be between 1 and 120.";

        // messages SendGrid
        public const string CakeShopEmail = "christina.b.nikolova@gmail.com";

        public const string ConfirmProfileTitleMessage = "Registration Confirmation";

        public const string ConfirmProfileMessage = "<p>{0}, thank you for your registration at <strong>CakeShop</strong>! Please, click <a href={1}>here</a> to confirm your email.</p>";

        public const string ForgotPasswordTitleMessage = "Reset Password";

        public const string ForgotPasswordMessage = "Please reset your password by <a href='{0}'>clicking here</a>.";

        // pagination
        public const int DessertsPerPage = 8;

        // basket
        public const string AddedToBasketSuccessfully = "Successfully added!";

        public const decimal EmptyBasketPrice = 0;

        // filter criterias
        public const string CriteriaPrice = "price";

        public const string CriteriaName = "name";

        // status messages
        public const string ProcessingStatus = "Processing";

        public const string ApprovedStatus = "Approved";

        public const string CancelledStatus = "Cancelled";

        public const string DeliveredStatus = "Delivered";

        // format time
        public const string DateTimeFormat = "{0:d}";

        public const string LocalTimeZone = "E. Europe Standard Time";

        // content lenght
        public const int RepiceShortDescriptionLength = 200;

        // alert messages
        public const string SuccessDeleteMessage = "Successfully deleted!";

        public const string SuccessUpdateMessage = "Successfully updated!";
    }
}
