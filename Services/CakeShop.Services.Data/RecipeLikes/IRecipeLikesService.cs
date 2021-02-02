namespace CakeShop.Services.Data.RecipeLikes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRecipeLikesService
    {
        Task<bool> IsFavouriteAsync(string id, string userId);

        Task<bool> LikeRecipeAsync(string recipeId, string userId);

        Task<IEnumerable<T>> UnlikeRecipeAsync<T>(string recipeId, string userId);

        Task<int> GetLikesCountAsync(string recipeId);
    }
}
