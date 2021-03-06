﻿namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;
    using CakeShop.Data.Models.Enums;

    public class Order : BaseDeletableModel<string>
    {
        public Order()
        {
            this.Id = Guid.NewGuid().ToString();
            this.OrderStatus = OrderStatus.NotFinish;
            this.IsPaid = false;
            this.TotalPrice = 0;
            this.IsReview = false;
            this.DessertOrders = new HashSet<DessertOrder>();
        }

        public decimal TotalPrice { get; set; }

        [Required]
        [MaxLength(DataValidation.OrderDeliveryAddressMaxLenght)]
        public string DeliveryAddress { get; set; }

        public bool IsPaid { get; set; }

        public OrderStatus OrderStatus { get; set; }

        [MaxLength(DataValidation.OrderNotesMaxLenght)]
        public string Notes { get; set; }

        public DateTime FinalizeOrder { get; set; }

        public int ReviewsCount { get; set; }

        public bool IsReview { get; set; }

        [Required]
        public string ClientId { get; set; }

        public virtual ApplicationUser Client { get; set; }

        public virtual ICollection<DessertOrder> DessertOrders { get; set; }
    }
}
