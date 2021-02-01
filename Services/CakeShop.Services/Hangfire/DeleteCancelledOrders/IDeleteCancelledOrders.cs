namespace CakeShop.Services.Hangfire.DeleteCancelledOrders
{
    using System.Threading.Tasks;

    public interface IDeleteCancelledOrders
    {
        Task DeleteAsync();
    }
}
