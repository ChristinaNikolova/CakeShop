namespace CakeShop.Services.Data.Categories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository<Category> categoriesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public CategoriesService(
            IRepository<Category> categoriesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.categoriesRepository = categoriesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var categories = await this.categoriesRepository
                .All()
                .OrderBy(c => c.Name)
                .To<T>()
                .ToListAsync();

            return categories;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllAsSelectListItemAsync()
        {
            var categories = await this.categoriesRepository
                .All()
                .Select(c => new SelectListItem()
                {
                    Value = c.Id,
                    Text = c.Name,
                })
                .ToListAsync();

            return categories;
        }

        public async Task<bool> AddAsync(string name, IFormFile picture, string description)
        {
            var isAdded = true;

            var isAlreadyAdded = await this.categoriesRepository
                .All()
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());

            if (isAlreadyAdded)
            {
                return !isAdded;
            }

            var pictureAsString = await this.GetPictureAsStringAsync(name, picture);

            var category = new Category()
            {
                Name = name,
                Description = description,
                Picture = pictureAsString,
            };

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            return isAdded;
        }

        public async Task<T> GetDetailsForUpdateAsync<T>(string id)
        {
            var category = await this.categoriesRepository
                .All()
                .Where(i => i.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return category;
        }

        public async Task UpdateAsync(string id, string name, IFormFile newPicture, string description)
        {
            var category = await this.GetByIdAsync(id);

            category.Name = name;
            category.Description = description;

            if (newPicture != null)
            {
                var pictureAsString = await this.GetPictureAsStringAsync(name, newPicture);
                category.Picture = pictureAsString;
            }

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var category = await this.GetByIdAsync(id);

            category.IsDeleted = true;

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task<string> GetPictureAsync(string id)
        {
            var picture = await this.categoriesRepository
                 .All()
                 .Where(d => d.Id == id)
                 .Select(d => d.Picture)
                 .FirstOrDefaultAsync();

            return picture;
        }

        private async Task<string> GetPictureAsStringAsync(string name, IFormFile picture)
        {
            return await this.cloudinaryService.UploudAsync(picture, name);
        }

        private async Task<Category> GetByIdAsync(string id)
        {
            return await this.categoriesRepository
               .All()
               .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
