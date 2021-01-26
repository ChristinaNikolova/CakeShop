namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public RecipesService(
            IRepository<Recipe> recipesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.recipesRepository = recipesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId)
        {
            var pictureAsString = await this.GetPictureAsStringAsync(title, picture);

            var recipe = new Recipe()
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                CookingTime = cookingTime,
                Picture = pictureAsString,
                PreparationTime = preparationTime,
                Portions = preparationTime,
            };

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var repices = await this.recipesRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .ThenBy(r => r.Title)
                .To<T>()
                .ToListAsync();

            return repices;
        }

        private async Task<string> GetPictureAsStringAsync(string name, IFormFile picture)
        {
            return await this.cloudinaryService.UploudAsync(picture, name);
        }
    }
}
