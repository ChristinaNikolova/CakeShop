namespace CakeShop.Services.Data.Comments
{
    using System.Threading.Tasks;

    public interface ICommentsService
    {
        Task AddAsync(string recipeId, string content, string userId);
    }
}
