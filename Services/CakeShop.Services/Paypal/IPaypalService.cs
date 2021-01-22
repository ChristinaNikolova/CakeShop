namespace CakeShop.Services.Paypal
{
    using System.Threading.Tasks;

    using PayPal.Api;

    public interface IPaypalService
    {
        Task<Payment> CreatePayment(decimal totalPrice);

        Task<Payment> ExecutePayment(string payerId, string paymentId, string token);
    }
}
