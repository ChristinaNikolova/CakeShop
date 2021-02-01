namespace CakeShop.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddAsync(string recipeId, string content, string userId);

        Task<IEnumerable<T>> GetCommentsCurrentRecipeAsync<T>(string recipeId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task DeleteAsync(string id);

        Task<int> GetNewCommentsCountAsync();
    }
}
