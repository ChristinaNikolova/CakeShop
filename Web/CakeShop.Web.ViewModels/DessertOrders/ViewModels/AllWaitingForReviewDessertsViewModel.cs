namespace CakeShop.Web.ViewModels.DessertOrders.ViewModels
{
    using System.Collections.Generic;

    public class AllWaitingForReviewDessertsViewModel
    {
        public IEnumerable<WaitingForReviewDessertViewModel> Desserts { get; set; }
    }
}
