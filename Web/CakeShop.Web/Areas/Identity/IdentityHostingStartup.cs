using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(CakeShop.Web.Areas.Identity.IdentityHostingStartup))]

namespace CakeShop.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}
