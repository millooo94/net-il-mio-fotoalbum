using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<IdentityUser>
    {
        public PhotoContext(DbContextOptions<PhotoContext> options) : base(options) { }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }

        public void Seed()
        {
            var photoSeed = new Photo[]
            {
                new Photo
                {
                    ImageUrl = "https://picsum.photos/200/300",
                    Title = "foto 1",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                    IsVisible = false,
                },
                new Photo
                {
                    ImageUrl = "https://picsum.photos/200/300",
                    Title = "foto 2",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                    IsVisible = false,
                },
                new Photo
                {
                    ImageUrl = "https://picsum.photos/200/300",
                    Title = "foto 3",
                    Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
                    IsVisible = false,
                },
            };

            if (!Photos.Any())
            {
                Photos.AddRange(photoSeed);
            }

            if (!Categories.Any())
            {
                var categorySeed = new Category[]
                {
                    new Category
                    {
                        Name = "Fotografia naturalistica",
                        Photos = photoSeed,
                    },
                    new Category
                    {
                        Name = "Fotografia paesaggistica",
                    },
                    new Category
                    {
                        Name = "Fotografia di animali domestici",
                    },
                    new Category
                    {
                        Name = "Astrofotografia",
                    },
                };

                Categories.AddRange(categorySeed);
            }
            SaveChanges();
        }

    }
}
