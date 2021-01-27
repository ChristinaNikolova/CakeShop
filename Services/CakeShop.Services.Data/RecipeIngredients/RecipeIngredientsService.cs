namespace CakeShop.Services.Data.RecipeIngredients
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Ingredients;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class RecipeIngredientsService : IRecipeIngredientsService
    {
        private readonly IRepository<RepiceIngredient> recipeIngredientsRepository;
        private readonly IIngredientsService ingredientsService;

        public RecipeIngredientsService(
            IRepository<RepiceIngredient> recipeIngredientsRepository,
            IIngredientsService ingredientsService)
        {
            this.recipeIngredientsRepository = recipeIngredientsRepository;
            this.ingredientsService = ingredientsService;
        }

        public async Task<bool> AddIngredientToRecipeAsync(string recipeId, string name, string quantity)
        {
            var isAdded = true;

            var ingredientId = await this.ingredientsService.GetIngredientIdByNameAsync(name);

            if (ingredientId == null)
            {
                return !isAdded;
            }

            var isAlreadyAdded = await this.recipeIngredientsRepository
                .All()
                .AnyAsync(ri => ri.IngredientId == ingredientId && ri.RecipeId == recipeId);

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var recipeIngredient = new RepiceIngredient()
            {
                RecipeId = recipeId,
                IngredientId = ingredientId,
                Quantity = quantity,
            };

            await this.recipeIngredientsRepository.AddAsync(recipeIngredient);
            await this.recipeIngredientsRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task<IEnumerable<T>> GetAllCurrentRecipeAsync<T>(string id)
        {
            var ingredients = await this.recipeIngredientsRepository
                .All()
                .Where(di => di.RecipeId == id)
                .OrderBy(di => di.Ingredient.Name)
                .To<T>()
                .ToListAsync();

            return ingredients;
        }

        public async Task RemoveIngredientFromRecipeAsync(string recipeId, string ingredientName)
        {
            var ingredientId = await this.ingredientsService.GetIngredientIdByNameAsync(ingredientName);

            var recipeIngredient = await this.recipeIngredientsRepository
                .All()
                .FirstOrDefaultAsync(ri => ri.RecipeId == recipeId && ri.IngredientId == ingredientId);

            this.recipeIngredientsRepository.Delete(recipeIngredient);
            await this.recipeIngredientsRepository.SaveChangesAsync();
        }
    }
}
