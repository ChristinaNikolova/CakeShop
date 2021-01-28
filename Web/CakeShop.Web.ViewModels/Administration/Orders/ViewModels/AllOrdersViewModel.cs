namespace CakeShop.Web.ViewModels.Administration.Orders.ViewModels
{
    using System.Collections.Generic;

    public class AllOrdersViewModel
    {
        public IEnumerable<OrderViewModel> ProcessingOrders { get; set; }

        public IEnumerable<OrderViewModel> ApprovedOrders { get; set; }

        public IEnumerable<OrderViewModel> DeliveredOrders { get; set; }

        public IEnumerable<OrderViewModel> CancelledOrders { get; set; }
    }
}
