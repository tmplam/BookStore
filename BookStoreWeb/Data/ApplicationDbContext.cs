using BookStoreWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreWeb.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var genres = new List<Genre>()
            {
                new Genre ()
                {
                    Id = Guid.Parse("5568b5ce-2e7f-4bc4-8af0-a18c976c9531"),
                    Name = "Business"
                },
                new Genre ()
                {
                    Id = Guid.Parse("b63e0779-7a0e-4b76-9469-8e27beebf898"),
                    Name = "Romance"
                },
                new Genre ()
                {
                    Id = Guid.Parse("2cd9ea1b-1b93-4977-8587-f9329f0e2078"),
                    Name = "Action & Adventure"
                },
                new Genre ()
                {
                    Id = Guid.Parse("3c232235-c2d6-42ed-b6ac-569e7f75a192"),
                    Name = "Mystery"
                },
                new Genre ()
                {
                    Id = Guid.Parse("86d4e2c3-10ad-4a90-be21-9ec3acec8fc9"),
                    Name = "Thriller"
                },
                new Genre ()
                {
                    Id = Guid.Parse("f45196b2-6137-4bd3-94b1-21175ece64c4"),
                    Name = "Computers & Technology"
                },
                new Genre ()
                {
                    Id = Guid.Parse("7621b5bf-97f8-4ff2-884b-9caccdcc8513"),
                    Name = "Humor & Entertainment"
                },
                new Genre ()
                {
                    Id = Guid.Parse("3c99605d-496b-4b96-a04a-094477ef2c4d"),
                    Name = "Art, Music & Photography"
                },
                new Genre ()
                {
                    Id = Guid.Parse("510bdcf5-f312-47e2-addf-9778c9d110d1"),
                    Name = "Medicine"
                },
                new Genre ()
                {
                    Id = Guid.Parse("e8350553-ebd1-4830-b0ee-81f85e1b22cc"),
                    Name = "Sports & Outdoors"
                }
            }; 
            modelBuilder.Entity<Genre>().HasData(genres);
        }
    }
}
