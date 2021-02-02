namespace CakeShop.Web
{
    using System;
    using System.Reflection;

    using CakeShop.Common;
    using CakeShop.Data;
    using CakeShop.Data.Common;
    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Repositories;
    using CakeShop.Data.Seeding;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Data.Categories;
    using CakeShop.Services.Data.Comments;
    using CakeShop.Services.Data.DessertIngredients;
    using CakeShop.Services.Data.DessertLikes;
    using CakeShop.Services.Data.DessertOrders;
    using CakeShop.Services.Data.Desserts;
    using CakeShop.Services.Data.DessertTags;
    using CakeShop.Services.Data.Ingredients;
    using CakeShop.Services.Data.Orders;
    using CakeShop.Services.Data.RecipeIngredients;
    using CakeShop.Services.Data.RecipeLikes;
    using CakeShop.Services.Data.Recipes;
    using CakeShop.Services.Data.Reviews;
    using CakeShop.Services.Data.Tags;
    using CakeShop.Services.Data.Users;
    using CakeShop.Services.Hangfire.DeleteCancelledOrders;
    using CakeShop.Services.Mapping;
    using CakeShop.Services.Messaging;
    using CakeShop.Services.Paypal;
    using CakeShop.Web.SecurityModels;
    using CakeShop.Web.ViewModels;
    using CloudinaryDotNet;
    using Hangfire;
    using Hangfire.Dashboard;
    using Hangfire.SqlServer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Rotativa.AspNetCore;
    using SendGrid;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Hangfire
            services.AddHangfire(
               config => config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
                   .UseSqlServerStorage(
                       this.configuration.GetConnectionString("DefaultConnection"),
                       new SqlServerStorageOptions
                       {
                           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                           QueuePollInterval = TimeSpan.Zero,
                           UseRecommendedIsolationLevel = true,
                           UsePageLocksOnDequeue = true,
                           DisableGlobalLocks = true,
                       }));

            // Add Db
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, SendGridEmailSender>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IPaypalService, PaypalService>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<ICategoriesService, CategoriesService>();
            services.AddTransient<IDessertsService, DessertsService>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IRecipesService, RecipesService>();
            services.AddTransient<IDessertIngredientsService, DessertIngredientsService>();
            services.AddTransient<IIngredientsService, IngredientsService>();
            services.AddTransient<IDessertTagsService, DessertTagsService>();
            services.AddTransient<ITagsService, TagsService>();
            services.AddTransient<IRecipeIngredientsService, RecipeIngredientsService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IDessertOrdersService, DessertOrdersService>();
            services.AddTransient<IRecipeLikesService, RecipeLikesService>();
            services.AddTransient<IDessertLikesService, DessertLikesService>();

            // Add Antiforgery
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });

            // Add Cloudinary
            var cloudinary = new Cloudinary(new Account()
            {
                Cloud = this.configuration["Cloudinary:AppName"],
                ApiKey = this.configuration["Cloudinary:AppKey"],
                ApiSecret = this.configuration["Cloudinary:AppSecret"],
            });

            services.AddSingleton(cloudinary);

            // Add reCAPTCHA
            services.Configure<GoogleReCAPTCHA>(this.configuration.GetSection("GoogleReCAPTCHA"));

            // Add SendGrid
            var sendGrid = new SendGridClient(this.configuration["SendGrid:ApiKey"]);
            services.AddSingleton(sendGrid);

            // Add Facebook Authentication
            services.AddAuthentication()
               .AddFacebook(option =>
               {
                   option.AppId = this.configuration["Facebook:AppId"];
                   option.AppSecret = this.configuration["Facebook:AppSecret"];
                   option.AccessDeniedPath = "/AccessDeniedPathInfo";
               });

            // Add Google Authentication
            services.AddAuthentication()
               .AddGoogle(option =>
               {
                   option.ClientId = this.configuration["Google:ClientId"];
                   option.ClientSecret = this.configuration["Google:ClientSecret"];
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            IRecurringJobManager recurringJobManager)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
                this.SeedHangfireJobs(recurringJobManager);
            }

            // Add Rotativa
            RotativaConfiguration.Setup(env.WebRootPath, GlobalConstants.Rotativa);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add Hangfire Dashboard
            app.UseHangfireServer(new BackgroundJobServerOptions { WorkerCount = 2 });
            app.UseHangfireDashboard(
                "/Administration/HangFire",
                new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } });

            // Routes
            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("dessertsByCategory", "{controller=Shop}/{action=GetAllCurrentCategory}/{id?}/{currentPage?}");
                        endpoints.MapRazorPages();
                    });
        }

        private void SeedHangfireJobs(IRecurringJobManager recurringJobManager)
        {
            recurringJobManager
                .AddOrUpdate<DeleteCancelledOrders>(
                "DeleteCancelledOrders", x => x.DeleteAsync(), Cron.Monthly);
        }

        private class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.User.IsInRole(GlobalConstants.AdministratorRoleName);
            }
        }
    }
}
