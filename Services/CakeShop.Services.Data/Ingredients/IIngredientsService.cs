namespace CakeShop.Services.Data.Ingredients
{
    using System.Threading.Tasks;

    public interface IIngredientsService
    {
        Task<string> GetIngredientIdByNameAsync(string name);
    }
}
