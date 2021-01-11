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
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class DessertsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Desserts.Any())
            {
                var dessertsData = JsonConvert
                    .DeserializeObject<List<DessertDto>>(File.ReadAllText(GlobalConstants.DessertSeederPath))
                    .ToList();

                List<Dessert> desserts = new List<Dessert>();
                List<DessertIngredient> dessertIngredients = new List<DessertIngredient>();
                List<DessertTag> dessertTags = new List<DessertTag>();

                foreach (var currentDessertData in dessertsData)
                {
                    var category = await dbContext.Categories
                        .FirstOrDefaultAsync(c => c.Name == currentDessertData.Category);

                    var dessert = new Dessert()
                    {
                        Name = currentDessertData.Name,
                        Description = currentDessertData.Description,
                        Price = currentDessertData.Price,
                        Picture = currentDessertData.Picture,
                    };

                    if (category != null)
                    {
                        dessert.CategoryId = category.Id;
                    }

                    foreach (var currentTag in currentDessertData.DessertTags)
                    {
                        var tag = await dbContext.Tags
                            .FirstOrDefaultAsync(sp => sp.Name == currentTag);

                        var dessertTag = new DessertTag()
                        {
                            DessertId = dessert.Id,
                            TagId = tag.Id,
                        };

                        dessertTags.Add(dessertTag);
                    }

                    foreach (var currentIngredient in currentDessertData.DessertIngredients)
                    {
                        var ingredient = await dbContext.Ingredients
                            .FirstOrDefaultAsync(sp => sp.Name == currentIngredient);

                        var dessertIngredient = new DessertIngredient()
                        {
                            DessertId = dessert.Id,
                            IngredientId = ingredient.Id,
                        };

                        dessertIngredients.Add(dessertIngredient);
                    }

                    desserts.Add(dessert);
                }
                ;
                await dbContext.DessertIngredients.AddRangeAsync(dessertIngredients);
                await dbContext.DessertTags.AddRangeAsync(dessertTags);
                await dbContext.Desserts.AddRangeAsync(desserts);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
