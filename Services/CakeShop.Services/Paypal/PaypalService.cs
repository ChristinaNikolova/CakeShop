﻿namespace CakeShop.Services.Paypal
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using PayPal.Api;

    public class PaypalService : IPaypalService
    {
        private readonly IConfiguration configuration;

        public PaypalService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Payment> CreatePayment(decimal totalPrice)
        {
            var apiContext = this.PreparePaypalConfigurations();

            try
            {
                var payment = new Payment
                {
                    intent = "sale",
                    payer = new Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            payee = new Payee
                            {
                                email = "softuni-beautysalon@abv.bg",
                            },
                            amount = new Amount
                            {
                                currency = "USD",
                                total = totalPrice.ToString(),
                            },
                            description = "Buying desserts from CakeShop.",
                        },
                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = @"https://localhost:44319/Paypal/FailedPayment",
                        return_url = $@"https://localhost:44319/Paypal/SuccessedPayment?totalPrice={totalPrice}",
                    },
                };

                var createdPayment = await Task.Run(() => payment.Create(apiContext));

                return createdPayment;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Payment> ExecutePayment(string payerId, string paymentId, string token)
        {
            var apiContext = this.PreparePaypalConfigurations();

            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            var payment = new Payment() { id = paymentId };

            var executedPayment = await Task.Run(() => payment.Execute(apiContext, paymentExecution));

            return executedPayment;
        }

        private APIContext PreparePaypalConfigurations()
        {
            var paypalConfugurations = new Dictionary<string, string>
            {
                { "mode", this.configuration["Paypal:Mode"] },
                { "clientId", this.configuration["Paypal:ClientId"] },
                { "clientSecret", this.configuration["Paypal:ClientSecret"] },
            };

            var accessToken = new OAuthTokenCredential(paypalConfugurations).GetAccessToken();

            var apiContext = new APIContext(accessToken)
            {
                Config = paypalConfugurations,
            };

            return apiContext;
        }
    }
}
