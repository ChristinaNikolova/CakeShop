namespace CakeShop.Services.Data.RecipeLikes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Recipes;
    using Microsoft.EntityFrameworkCore;

    public class RecipeLikesService : IRecipeLikesService
    {
        private readonly IRepository<RecipeLike> recipeLikesRepository;
        private readonly IRecipesService recipesService;

        public RecipeLikesService(
            IRepository<RecipeLike> recipeLikesRepository,
            IRecipesService recipesService)
        {
            this.recipeLikesRepository = recipeLikesRepository;
            this.recipesService = recipesService;
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

        public async Task<IEnumerable<T>> UnlikeRecipeAsync<T>(string recipeId, string userId)
        {
            await this.RemoveFromFavouriteAsync(recipeId, userId, true);

            await this.recipeLikesRepository.SaveChangesAsync();

            var favouriteDesserts = await this.recipesService.GetUserFavouriteRecipesAsync<T>(userId);

            return favouriteDesserts;
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
