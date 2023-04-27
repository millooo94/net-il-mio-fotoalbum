using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace net_il_mio_fotoalbum.Models
{
    public class PhotoContext : IdentityDbContext<User>
    {
        public PhotoContext(DbContextOptions<PhotoContext> options) : base(options) { }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Photo>()
                .HasOne(p => p.User)
                .WithMany(u => u.Photos)
                .HasForeignKey(p => p.UserId);
        }


        public void Seed()
        {
            //var photoSeed = new Photo[]
            //{
            //    new Photo
            //    {
            //        ImageUrl = "https://picsum.photos/200/300",
            //        Title = "foto 1",
            //        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
            //        IsVisible = false,
            //    },
            //    new Photo
            //    {
            //        ImageUrl = "https://picsum.photos/200/300",
            //        Title = "foto 2",
            //        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
            //        IsVisible = false,
            //    },
            //    new Photo
            //    {
            //        ImageUrl = "https://picsum.photos/200/300",
            //        Title = "foto 3",
            //        Description = "Lorem, ipsum dolor sit amet consectetur adipisicing elit. Mollitia quis est adipisci incidunt rem nostrum ipsam fuga ratione tempora eveniet!",
            //        IsVisible = false,
            //    },
            //};

            //if (!Photos.Any())
            //{
            //    Photos.AddRange(photoSeed);
            //}

            //if (!Categories.Any())
            //{
            //    var categorySeed = new Category[]
            //    {
            //        new Category
            //        {
            //            Name = "Fotografia naturalistica",
            //            Photos = photoSeed,
            //        },
            //        new Category
            //        {
            //            Name = "Fotografia paesaggistica",
            //        },
            //        new Category
            //        {
            //            Name = "Fotografia di animali domestici",
            //        },
            //        new Category
            //        {
            //            Name = "Astrofotografia",
            //        },
            //    };

            //    Categories.AddRange(categorySeed);
            //}
            if (!Roles.Any())
            {
                var seed = new IdentityRole[]
                {
                    new("SuperAdmin"),
                    new("Admin"),
                };

                Roles.AddRange(seed);
            }

            //if (Users.Any(u => u.Email == "superadmin@dev.com" || u.Email == "admin@dev.com")
            //    && !UserRoles.Any())
            //{
            //    var superadmin = Users.First(u => u.Email == "superadmin@dev.com");
            //    var admin = Users.First(u => u.Email == "admin@dev.com");

            //    var superAdminRole = Roles.First(r => r.Name == "SuperAdmin");

            //    var seed = new IdentityUserRole<string>[]
            //    {
            //        new()
            //        {
            //            UserId = superadmin.Id,
            //            RoleId = superAdminRole.Id
            //        },
            //    };

            //    UserRoles.AddRange(seed);
            //}
            SaveChanges();
        }

    }
}
