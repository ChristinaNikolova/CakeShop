namespace CakeShop.Services.Data.DessertIngredients
{
    using System;
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

        public async Task<bool> AddAsync(string dessertId, string name)
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
    }
}
