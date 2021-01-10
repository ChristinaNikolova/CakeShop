namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CupcakeLikes = new HashSet<CupcakeLike>();
            this.ArticleLikes = new HashSet<RecipeLike>();
            this.Orders = new HashSet<Order>();
            this.Reviews = new HashSet<Review>();
            this.Comments = new HashSet<Comment>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        [Required]
        [MaxLength(DataValidation.UserFirstNameMaxLenght)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(DataValidation.UserLastNameMaxLenght)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(DataValidation.UserAddressMaxLenght)]
        public string Address { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<CupcakeLike> CupcakeLikes { get; set; }

        public virtual ICollection<RecipeLike> ArticleLikes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    }
}
