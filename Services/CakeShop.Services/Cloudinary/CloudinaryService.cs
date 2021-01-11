namespace CakeShop.Services.Cloudinary
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinary;

        public CloudinaryService(Cloudinary cloudinary)
        {
            this.cloudinary = cloudinary;
        }

        [Obsolete]
        public async Task<string> UploudAsync(IFormFile image, string imageName)
        {
            byte[] destinationImage;

            using (var memoryStream = new MemoryStream())
            {
                await image.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            using (var ms = new MemoryStream(destinationImage))
            {
                imageName += DateTime.UtcNow.ToString();

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imageName, ms),
                    PublicId = imageName,
                };

                var uploadResult = this.cloudinary.UploadAsync(uploadParams);

                return uploadResult.Result.SecureUri.AbsoluteUri;
            }
        }
    }
}
