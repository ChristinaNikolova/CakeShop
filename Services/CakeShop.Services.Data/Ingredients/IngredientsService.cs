namespace CakeShop.Services.Data.Ingredients
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class IngredientsService : IIngredientsService
    {
        private readonly IRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
        }

        public async Task<bool> AddAsync(string name)
        {
            var isAdded = true;

            var isAlreadyAdded = await this.ingredientsRepository
                 .All()
                 .AnyAsync(i => i.Name.ToLower() == name.ToLower());

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var ingredient = new Ingredient()
            {
                Name = name,
            };

            await this.ingredientsRepository.AddAsync(ingredient);
            await this.ingredientsRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task DeleteAsync(string id)
        {
            var ingredient = await this.GetByIdAsync(id);

            ingredient.IsDeleted = true;

            this.ingredientsRepository.Update(ingredient);
            await this.ingredientsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var ingredients = await this.ingredientsRepository
                .All()
                .OrderBy(i => i.Name)
                .To<T>()
                .ToListAsync();

            return ingredients;
        }

        public async Task<T> GetDetailsForUpdateAsync<T>(string id)
        {
            var ingredient = await this.ingredientsRepository
                .All()
                .Where(i => i.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return ingredient;
        }

        public async Task<string> GetIngredientIdByNameAsync(string name)
        {
            var id = await this.ingredientsRepository
                .All()
                .Where(i => i.Name.ToLower() == name.ToLower())
                .Select(i => i.Id)
                .FirstOrDefaultAsync();

            return id;
        }

        public async Task UpdateAsync(string id, string name)
        {
            var ingredient = await this.GetByIdAsync(id);

            ingredient.Name = name;

            this.ingredientsRepository.Update(ingredient);
            await this.ingredientsRepository.SaveChangesAsync();
        }

        private async Task<Ingredient> GetByIdAsync(string id)
        {
            return await this.ingredientsRepository
                .All()
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
