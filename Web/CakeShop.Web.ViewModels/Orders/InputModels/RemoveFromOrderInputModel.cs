namespace CakeShop.Web.ViewModels.Orders.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class RemoveFromOrderInputModel
    {
        [Required]
        public string Id { get; set; }
    }
}
