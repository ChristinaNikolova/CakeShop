namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
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
        private readonly IRepository<RecipeLike> recipeLikesRepository;

        public RecipesService(
            CakeShop.Data.Common.Repositories.IRepository<Recipe> recipesRepository,
            ICloudinaryService cloudinaryService,
            CakeShop.Data.Common.Repositories.IRepository<RecipeLike> recipeLikesRepository)
        {
            this.recipesRepository = recipesRepository;
            this.cloudinaryService = cloudinaryService;
            this.recipeLikesRepository = recipeLikesRepository;
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
                Portions = portions,
            };

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string title, string content, IFormFile newPicture, int portions, int cookingTime, int preparationTime, string categoryId)
        {
            var recipe = await this.GetByIdAsync(id);

            recipe.Title = title;
            recipe.Content = content;
            recipe.Portions = portions;
            recipe.CategoryId = categoryId;
            recipe.CookingTime = cookingTime;
            recipe.PreparationTime = preparationTime;

            if (newPicture != null)
            {
                var pictureAsString = await this.GetPictureAsStringAsync(title, newPicture);
                recipe.Picture = pictureAsString;
            }

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var recipe = await this.GetByIdAsync(id);

            recipe.IsDeleted = true;

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task<T> GetDetailsAsync<T>(string id)
        {
            var recipe = await this.recipesRepository
                .All()
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return recipe;
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

        public async Task<string> GetPictureAsync(string id)
        {
            var picture = await this.recipesRepository
                 .All()
                 .Where(d => d.Id == id)
                 .Select(d => d.Picture)
                 .FirstOrDefaultAsync();

            return picture;
        }

        public async Task<bool> IsFavouriteAsync(string id, string userId)
        {
            var isFavourite = await this.recipeLikesRepository
               .All()
               .AnyAsync(rl => rl.RecipeId == id && rl.ClientId == userId);

            return isFavourite;
        }

        public async Task<int> GetLikesCountAsync(string recipeId)
        {
            var count = await this.recipeLikesRepository
                .All()
                .CountAsync(rl => rl.RecipeId == recipeId);

            return count;
        }

        public async Task<IEnumerable<T>> GetRecentRecipesAsync<T>()
        {
            var recipes = await this.recipesRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .Take(GlobalConstants.SidebarRecipesCount)
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<T>> GetPopulartRecipesAsync<T>()
        {
            var recipes = await this.recipesRepository
                .All()
                .OrderByDescending(r => r.RecipeLikes.Count())
                .ThenBy(r => r.Title)
                .Take(GlobalConstants.SidebarRecipesCount)
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<T>> GetByCategoryAsync<T>(string categoryId)
        {
            var recipes = await this.recipesRepository
                 .All()
                 .Where(r => r.CategoryId == categoryId)
                 .OrderByDescending(r => r.CreatedOn)
                 .ThenBy(r => r.Title)
                 .To<T>()
                 .ToListAsync();

            return recipes;
        }

        public async Task<bool> LikeRecipeAsync(string recipeId, string userId)
        {
            var isAdded = true;
            var isExisting = await this.IsFavouriteAsync(recipeId, userId);

            if (isExisting)
            {
                isAdded = await this.RemoveFromFavouriteAsync(recipeId, userId, isAdded);
            }
            else
            {
                await this.AddToFavouriteAsync(recipeId, userId);
            }

            await this.recipeLikesRepository.SaveChangesAsync();

            return isAdded;
        }

        private async Task<string> GetPictureAsStringAsync(string name, IFormFile picture)
        {
            return await this.cloudinaryService.UploudAsync(picture, name);
        }

        private async Task<Recipe> GetByIdAsync(string id)
        {
            return await this.recipesRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        private async Task AddToFavouriteAsync(string recipeId, string userId)
        {
            var recipeLike = new RecipeLike()
            {
                ClientId = userId,
                RecipeId = recipeId,
            };

            await this.recipeLikesRepository.AddAsync(recipeLike);
        }

        private async Task<bool> RemoveFromFavouriteAsync(string recipeId, string userId, bool isAdded)
        {
            var recipeLike = await this.recipeLikesRepository
                                .All()
                                .FirstOrDefaultAsync(rl => rl.RecipeId == recipeId && rl.ClientId == userId);

            isAdded = false;
            this.recipeLikesRepository.Delete(recipeLike);

            return isAdded;
        }
    }
}
