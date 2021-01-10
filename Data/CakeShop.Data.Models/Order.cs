namespace CakeShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;
    using CakeShop.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(DataValidation.OrderDeliveryAddressMaxLenght)]
        public string DeliveryAddress { get; set; }

        public bool IsPaid { get; set; }

        public Status Status { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }
    }
}
