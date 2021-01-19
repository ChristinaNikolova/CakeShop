namespace CakeShop.Web.ViewModels.Desserts.InputModels
{
    using System.ComponentModel.DataAnnotations;

    public class OrderDessertsInputModel
    {
        [Required]
        public string TargetCriteria { get; set; }

        [Required]
        public string CategoryId { get; set; }
    }
}
