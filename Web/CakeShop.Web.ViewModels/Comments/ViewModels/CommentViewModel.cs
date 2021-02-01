namespace CakeShop.Web.ViewModels.Comments.ViewModels
{
    using System;

    using CakeShop.Data.Models;
    using CakeShop.Services.Mapping;

    public class CommentViewModel : IMapFrom<Comment>
    {
        public string Content { get; set; }

        public string ClientUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string FormattedCreatedOn
            => this.CreatedOn.ToShortDateString();
    }
}
