namespace CakeShop.Services.Data.Tags
{
    using System.Threading.Tasks;

    public interface ITagsService
    {
        Task<string> GetTagIdByNameAsync(string name);
    }
}
