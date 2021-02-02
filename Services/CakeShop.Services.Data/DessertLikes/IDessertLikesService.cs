namespace CakeShop.Services.Data.DessertLikes
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDessertLikesService
    {
        Task<bool> LikeDessertAsync(string dessertId, string userId);

        Task<bool> IsFavouriteAsync(string dessertId, string userId);

        Task<IEnumerable<T>> UnlikeDessertAsync<T>(string dessertId, string userId);
    }
}
