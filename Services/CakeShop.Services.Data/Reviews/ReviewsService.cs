namespace CakeShop.Services.Data.Reviews
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Data.Common.Repositories;
    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> reviewsRepository;

        public ReviewsService(IRepository<Review> reviewsRepository)
        {
            this.reviewsRepository = reviewsRepository;
        }

        public async Task<IEnumerable<T>> GetReviewsCurrentDessertAsync<T>(string dessertId)
        {
            var reviews = await this.reviewsRepository
                .All()
                .Where(r => r.DessertId == dessertId)
                .OrderByDescending(r => r.CreatedOn)
                .To<T>()
                .ToListAsync();

            return reviews;
        }
    }
}
