﻿namespace CakeShop.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;

    public class UsersToRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            await SeedRoleAsync(userManager, GlobalConstants.AdministratorRoleName, GlobalConstants.AdminEmail);
        }

        private static async Task SeedRoleAsync(UserManager<ApplicationUser> userManager, string roleName, string userEmail)
        {
            var users = userManager.Users
                .Where(u => u.Email.Contains(userEmail))
                .ToList();

            foreach (var user in users)
            {
                if (user.Roles.Any() == false)
                {
                    await userManager.AddToRoleAsync(user, roleName);
                }
            }
        }
    }
}
