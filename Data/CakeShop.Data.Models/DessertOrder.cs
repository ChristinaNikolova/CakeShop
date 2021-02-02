namespace CakeShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Data.Common.Models;

    public class DessertOrder : BaseDeletableModel<string>
    {
        public DessertOrder()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsReview = false;
        }

        [Required]
        public string DessertId { get; set; }

        public virtual Dessert Dessert { get; set; }

        [Required]
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public int Quantity { get; set; }

        public bool IsReview { get; set; }
    }
}
