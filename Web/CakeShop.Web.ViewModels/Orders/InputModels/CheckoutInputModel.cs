namespace CakeShop.Web.ViewModels.Orders.InputModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Web.ViewModels.Desserts.ViewModels;
    using CakeShop.Web.ViewModels.Orders.ViewModels;
    using CakeShop.Web.ViewModels.Users.ViewModels;

    public class CheckoutInputModel : OrderTotalPriceAndQuantitiesViewModel
    {
        public UserCheckoutViewModel User { get; set; }

        public IEnumerable<DessertBaseViewModel> Desserts { get; set; }

        [MaxLength(DataValidation.OrderNotesMaxLenght)]
        public string Notes { get; set; }

        [Required]
        [Display(Name = "Delivery Address")]
        [StringLength(DataValidation.OrderDeliveryAddressMaxLenght, ErrorMessage = GlobalConstants.ErrorMessage, MinimumLength = DataValidation.OrderDeliveryAddressMinLenght)]
        public string DeliveryAddress { get; set; }
    }
}
