namespace CakeShop.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Data.Seeding.Dtos;
    using Newtonsoft.Json;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Categories.Any())
            {
                var categoriesData = JsonConvert
                    .DeserializeObject<List<CategoryDto>>(File.ReadAllText(GlobalConstants.CategorySeederPath))
                    .ToList();

                List<Category> categories = new List<Category>();

                foreach (var currentCategoryData in categoriesData)
                {
                    var category = new Category()
                    {
                        Name = currentCategoryData.Name,
                        Picture = currentCategoryData.Picture,
                    };

                    categories.Add(category);
                }

                await dbContext.Categories.AddRangeAsync(categories);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
