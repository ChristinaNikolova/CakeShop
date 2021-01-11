namespace CakeShop.Data.Seeding.CustomSeeders
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CakeShop.Common;
    using CakeShop.Data.Models;
    using CakeShop.Data.Seeding.Dtos;
    using Newtonsoft.Json;

    public class TagsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Tags.Any())
            {
                var tagsData = JsonConvert
                    .DeserializeObject<List<TagDto>>(File.ReadAllText(GlobalConstants.TagSeederPath))
                    .ToList();

                List<Tag> tags = new List<Tag>();

                foreach (var currentTagData in tagsData)
                {
                    var tag = new Tag()
                    {
                        Name = currentTagData.Name,
                    };

                    tags.Add(tag);
                }

                await dbContext.Tags.AddRangeAsync(tags);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
