namespace CakeShop.Services.Cloudinary
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploudAsync(IFormFile image, string imageName);
    }
}
