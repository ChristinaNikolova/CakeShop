namespace CakeShop.Services.Data.DessertIngredients
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Data.Ingredients;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class DessertIngredientsService : IDessertIngredientsService
    {
        private readonly IRepository<DessertIngredient> dessertIngredientsRepository;
        private readonly IIngredientsService ingredientsService;

        public DessertIngredientsService(
            IRepository<DessertIngredient> dessertIngredientsRepository,
            IIngredientsService ingredientsService)
        {
            this.dessertIngredientsRepository = dessertIngredientsRepository;
            this.ingredientsService = ingredientsService;
        }

        public async Task<bool> AddIngredientToDessertAsync(string dessertId, string name)
        {
            var isAdded = true;

            var ingredientId = await this.ingredientsService.GetIngredientIdByNameAsync(name);

            if (ingredientId == null)
            {
                return !isAdded;
            }

            var isAlreadyAdded = await this.dessertIngredientsRepository
                .All()
                .AnyAsync(di => di.IngredientId == ingredientId && di.DessertId == dessertId);

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var dessertIngredient = new DessertIngredient()
            {
                DessertId = dessertId,
                IngredientId = ingredientId,
            };

            await this.dessertIngredientsRepository.AddAsync(dessertIngredient);
            await this.dessertIngredientsRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task<IEnumerable<T>> GetAllCurrentDessertAsync<T>(string dessertId)
        {
            var ingredients = await this.dessertIngredientsRepository
                .All()
                .Where(di => di.DessertId == dessertId)
                .OrderBy(di => di.Ingredient.Name)
                .To<T>()
                .ToListAsync();

            return ingredients;
        }

        public async Task RemoveIngredientFromDessertAsync(string dessertId, string ingredientName)
        {
            var ingredientId = await this.ingredientsService.GetIngredientIdByNameAsync(ingredientName);

            var dessertIngredient = await this.dessertIngredientsRepository
                .All()
                .FirstOrDefaultAsync(di => di.DessertId == dessertId && di.IngredientId == ingredientId);

            this.dessertIngredientsRepository.Delete(dessertIngredient);
            await this.dessertIngredientsRepository.SaveChangesAsync();
        }
    }
}
