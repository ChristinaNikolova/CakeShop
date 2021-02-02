namespace CakeShop.Services.Hangfire.DeleteCancelledOrders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using Microsoft.EntityFrameworkCore;

    public class DeleteCancelledOrders : IDeleteCancelledOrders
    {
        private readonly IDeletableEntityRepository<Order> ordersRepository;

        public DeleteCancelledOrders(IDeletableEntityRepository<Order> ordersRepository)
        {
            this.ordersRepository = ordersRepository;
        }

        public async Task DeleteAsync()
        {
            var ordersToDelete = await this.ordersRepository
                .All()
                .Where(o => o.FinalizeOrder.AddDays(GlobalConstants.DaysOneMonth).Date <= DateTime.Today.Date
                         && o.OrderStatus == OrderStatus.Cancelled)
                .ToListAsync();

            foreach (var order in ordersToDelete)
            {
                order.IsDeleted = true;
                this.ordersRepository.Update(order);
            }

            await this.ordersRepository.SaveChangesAsync();
        }
    }
}
