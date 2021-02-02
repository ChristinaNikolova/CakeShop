namespace CakeShop.Services.Data.Comments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Data.Models.Enums;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class CommentsService : ICommentsService
    {
        private readonly IRepository<Comment> commentsRepository;

        public CommentsService(IRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public async Task AddAsync(string recipeId, string content, string userId)
        {
            var comment = new Comment()
            {
                Content = content,
                RecipeId = recipeId,
                ClientId = userId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var comment = await this.commentsRepository
                .All()
                .FirstOrDefaultAsync(c => c.Id == id);

            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllUnapprovedAsync<T>()
        {
            var comments = await this.commentsRepository
                .All()
                .Where(c => c.CommentStatus == CommentStatus.NotApproved)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<T>> GetCommentsCurrentRecipeAsync<T>(string recipeId)
        {
            var comments = await this.commentsRepository
                .All()
                .Where(c => c.RecipeId == recipeId
                         && c.CommentStatus == CommentStatus.Approved)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

            return comments;
        }

        public async Task<int> GetNewCommentsCountAsync()
        {
            var count = await this.commentsRepository
                .All()
                .Where(c => c.CommentStatus == CommentStatus.NotApproved)
                .CountAsync();

            return count;
        }
    }
}
