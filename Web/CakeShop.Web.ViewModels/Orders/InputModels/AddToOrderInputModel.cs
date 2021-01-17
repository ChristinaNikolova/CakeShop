namespace CakeShop.Web.ViewModels.Orders.InputModels
{
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;

    public class AddToOrderInputModel
    {
        [Required]
        public string DessertId { get; set; }

        [Range(typeof(int), DataValidation.MinQuantity, DataValidation.MaxQuantity)]
        public int Quantity { get; set; }
    }
}
