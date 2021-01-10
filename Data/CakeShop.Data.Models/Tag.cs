namespace CakeShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using CakeShop.Common;
    using CakeShop.Data.Common.Models;

    public class Tag : BaseDeletableModel<string>
    {
        public Tag()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CupcakeTags = new HashSet<CupcakeTag>();
        }

        [Required]
        [MaxLength(DataValidation.TagNameMaxLenght)]
        public string Name { get; set; }

        public virtual ICollection<CupcakeTag> CupcakeTags { get; set; }
    }
}
