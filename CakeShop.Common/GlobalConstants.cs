namespace CakeShop.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "CakeShop";

        public const string AdministratorRoleName = "Administrator";

        public const string AdminName = "Admin";

        public const string AdminEmail = "admin@cakeshop.com";

        public const string AdminPicture = "https://res.cloudinary.com/dieu4mste/image/upload/v1612248363/logo_n5n55j.png";

        public const string SystemPasswordHashed = "AQAAAAEAACcQAAAAECrjCD23cQQ28Tyci+UMuaGrFMDUb/trG4E0RbJa4McRVfWFJ6c5UG4NpbXDB6K5rQ==";

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

        public const string SendRecipeAsPdfTitleMessage = "Recipe File";

        public const string SendRecipeAsPdf = "Thank you! You can find the recipe in the attached files!";

        public const string OrderConfirmationTitle = "Order confirmation";

        public const string OrderConfirmation = "Your order is confirmed!";

        // pagination
        public const int DessertsPerPage = 8;

        public const int RecipesPerPage = 5;

        public const int OrdersPerPage = 5;

        // basket
        public const string AddedToBasketSuccessfully = "Successfully added!";

        public const decimal EmptyBasketPrice = 0;

        // filter criterias
        public const string CriteriaPrice = "price";

        public const string CriteriaName = "name";

        public const string CriteriaTitle = "Title";

        public const string CriteriaNewest = "Newest";

        public const string CriteriaOldest = "Oldest";

        public const string CriteriaLikes = "Likes";

        public const string CriteriaComments = "Comments";

        // recipes sidebar criterias
        public const string CriteriaRecent = "recent";

        public const string CriteriaPopular = "popular";

        // blog
        public const int SidebarRecipesCount = 5;

        // status messages
        public const string ProcessingStatus = "Processing";

        public const string ApprovedStatus = "Approved";

        public const string CancelledStatus = "Cancelled";

        public const string DeliveredStatus = "Delivered";

        // content lenght
        public const int RepiceShortDescriptionLength = 200;

        // alert messages
        public const string SuccessDeleteMessage = "Successfully deleted!";

        public const string SuccessAddedMessage = "Successfully added!";

        public const string SuccessUpdateMessage = "Successfully updated!";

        public const string ProblemWithAddingIngredient = "Ingredient doesn't exist or ingredient is already added!";

        public const string ProblemWithAddingTag = "Tag doesn't exist or tag is already added!";

        public const string AlreadyExistingCategory = "Category already exists!";

        public const string AlreadyExistingIngredient = "Ingredient already exists!";

        public const string AlreadyExistingTag = "Tag already exists!";

        public const string PaymentSuccess = "Your payment is successful! We will inform you per email, when your order is approved.";

        public const string SuccessSendRecipeMessage = "Successfully send!";

        public const string SuccessAddedComment = "Your comment is created, but it isn't approve by Admin yet!";

        // rotativa
        public const string Rotativa = "Rotativa";

        public const string GenerateRecipePdfViewName = "GenerateRecipePdf";

        public const string GenerateOrderPdfViewName = "GenerateOrderPdf";

        public const string PdfMimeType = "application/pdf";

        // hangfire
        public const int DaysOneMonth = 30;
    }
}
