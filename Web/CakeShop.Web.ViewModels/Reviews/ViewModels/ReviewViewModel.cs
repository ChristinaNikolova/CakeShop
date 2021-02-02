namespace CakeShop.Web.ViewModels.Reviews.ViewModels
{
    using System;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class ReviewViewModel : IMapFrom<Review>
    {
        public string Content { get; set; }

        public int Points { get; set; }

        public string ClientUserName { get; set; }

        public string ClientPicture { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedOnAsString
            => string.Format("{0:d}", this.CreatedOn);
    }
}
