namespace CakeShop.Services.Data.Comments
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
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

        public async Task<IEnumerable<T>> GetCommentsCurrentRecipeAsync<T>(string recipeId)
        {
            var comments = await this.commentsRepository
                .All()
                .Where(c => c.RecipeId == recipeId)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

            return comments;
        }
    }
}
