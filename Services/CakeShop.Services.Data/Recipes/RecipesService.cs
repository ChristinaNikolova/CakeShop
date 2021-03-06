﻿namespace CakeShop.Services.Data.Recipes
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Cloudinary;
    using CakeShop.Services.Mapping;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;

    public class RecipesService : IRecipesService
    {
        private readonly IRepository<Recipe> recipesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public RecipesService(
            CakeShop.Data.Common.Repositories.IRepository<Recipe> recipesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.recipesRepository = recipesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task AddAsync(string title, string content, IFormFile picture, int portions, int preparationTime, int cookingTime, string categoryId)
        {
            var pictureAsString = await this.GetPictureAsStringAsync(title, picture);

            var recipe = new Recipe()
            {
                Title = title,
                Content = content,
                CategoryId = categoryId,
                CookingTime = cookingTime,
                Picture = pictureAsString,
                PreparationTime = preparationTime,
                Portions = portions,
            };

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(string id, string title, string content, IFormFile newPicture, int portions, int cookingTime, int preparationTime, string categoryId)
        {
            var recipe = await this.GetByIdAsync(id);

            recipe.Title = title;
            recipe.Content = content;
            recipe.Portions = portions;
            recipe.CategoryId = categoryId;
            recipe.CookingTime = cookingTime;
            recipe.PreparationTime = preparationTime;

            if (newPicture != null)
            {
                var pictureAsString = await this.GetPictureAsStringAsync(title, newPicture);
                recipe.Picture = pictureAsString;
            }

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var recipe = await this.GetByIdAsync(id);

            recipe.IsDeleted = true;

            this.recipesRepository.Update(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public async Task<T> GetDetailsAsync<T>(string id)
        {
            var recipe = await this.recipesRepository
                .All()
                .Where(d => d.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();

            return recipe;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(int take = 0, int skip = 0)
        {
            var query = this.recipesRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .ThenBy(r => r.Title)
                .AsQueryable();

            if (take != 0)
            {
                query = query
                 .Skip(skip)
                 .Take(take)
                 .AsQueryable();
            }

            var repices = await query
                .To<T>()
                .ToListAsync();

            return repices;
        }

        public async Task<string> GetPictureAsync(string id)
        {
            var picture = await this.recipesRepository
                 .All()
                 .Where(d => d.Id == id)
                 .Select(d => d.Picture)
                 .FirstOrDefaultAsync();

            return picture;
        }

        public async Task<IEnumerable<T>> GetRecentRecipesAsync<T>()
        {
            var recipes = await this.recipesRepository
                .All()
                .OrderByDescending(r => r.CreatedOn)
                .Take(GlobalConstants.SidebarRecipesCount)
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<T>> GetPopulartRecipesAsync<T>()
        {
            var recipes = await this.recipesRepository
                .All()
                .OrderByDescending(r => r.RecipeLikes.Count())
                .ThenBy(r => r.Title)
                .Take(GlobalConstants.SidebarRecipesCount)
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<T>> GetByCategoryAsync<T>(string categoryId)
        {
            var recipes = await this.recipesRepository
                 .All()
                 .Where(r => r.CategoryId == categoryId)
                 .OrderByDescending(r => r.CreatedOn)
                 .ThenBy(r => r.Title)
                 .To<T>()
                 .ToListAsync();

            return recipes;
        }

        public async Task<int> GetTotalCountRecipesAsync()
        {
            int recipesCount = await this.recipesRepository
                .All()
                .CountAsync();

            return recipesCount;
        }

        public async Task<IEnumerable<T>> GetUserFavouriteRecipesAsync<T>(string userId)
        {
            var recipes = await this.recipesRepository
                .All()
                .Where(r => r.RecipeLikes.Any(rl => rl.ClientId == userId))
                .OrderByDescending(r => r.CreatedOn)
                .ThenBy(r => r.Title)
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        public async Task<IEnumerable<T>> OrderRecipesByCriteriaAsync<T>(string criteria)
        {
            var criteriaLowerCase = criteria.ToLower();

            var query = this.recipesRepository
                .All()
                .AsQueryable();

            if (criteriaLowerCase == GlobalConstants.CriteriaTitle.ToLower())
            {
                query = query
                    .OrderBy(q => q.Title)
                    .ThenByDescending(q => q.CreatedOn)
                    .AsQueryable();
            }
            else if (criteriaLowerCase == GlobalConstants.CriteriaNewest.ToLower())
            {
                query = query
                    .OrderByDescending(q => q.CreatedOn)
                    .ThenBy(q => q.Title)
                    .AsQueryable();
            }
            else if (criteriaLowerCase == GlobalConstants.CriteriaOldest.ToLower())
            {
                query = query
                    .OrderBy(q => q.CreatedOn)
                    .ThenBy(q => q.Title)
                    .AsQueryable();
            }
            else if (criteriaLowerCase == GlobalConstants.CriteriaLikes.ToLower())
            {
                query = query
                    .OrderByDescending(q => q.RecipeLikes.Count())
                    .ThenBy(q => q.Title)
                    .AsQueryable();
            }
            else if (criteriaLowerCase == GlobalConstants.CriteriaComments.ToLower())
            {
                query = query
                    .OrderByDescending(q => q.Comments.Count())
                    .ThenBy(q => q.Title)
                    .AsQueryable();
            }

            var recipes = await query
                .To<T>()
                .ToListAsync();

            return recipes;
        }

        private async Task<string> GetPictureAsStringAsync(string name, IFormFile picture)
        {
            return await this.cloudinaryService.UploudAsync(picture, name);
        }

        private async Task<Recipe> GetByIdAsync(string id)
        {
            return await this.recipesRepository
                .All()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
