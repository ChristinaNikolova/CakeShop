namespace CakeShop.Services.Data.Ingredients
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class IngredientsService : IIngredientsService
    {
        private readonly IRepository<Ingredient> ingredientsRepository;

        public IngredientsService(IRepository<Ingredient> ingredientsRepository)
        {
            this.ingredientsRepository = ingredientsRepository;
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
    }
}
