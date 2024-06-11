using kolokwiumDrugie.Models;
namespace kolokwiumDrugie.Data;
using Microsoft.EntityFrameworkCore;


    public class DatabaseContext : DbContext
    {
        protected DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }
        
        

        public DbSet<Characters> Characters { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Backpacks> Backpacks { get; set; }
        public DbSet<Titles> Titles { get; set; }
        public DbSet<Character_titles> CharacterTitles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
            modelBuilder.Entity<Backpacks>()
                .HasKey(bp => new { bp.CharacterId, bp.ItemId });

            modelBuilder.Entity<Character_titles>()
                .HasKey(ct => new { ct.CharacterId, ct.TitleId });


            modelBuilder.Entity<Characters>().HasData(
                new Characters { Id = 1, FirstName = "John", LastName = "Kowalski", CurrentWeight = 43, MaxWeight = 200 },
                new Characters { Id = 2, FirstName = "Leo", LastName = "Messi", CurrentWeight = 70, MaxWeight = 175 }

            );

            modelBuilder.Entity<Items>().HasData(
                new Items { Id = 1, Name = "widelec", Weight = 10 },
                new Items { Id = 2, Name = "marchew", Weight = 11 },
                new Items { Id = 3, Name = "noga", Weight = 12 }
            );

            modelBuilder.Entity<Backpacks>().HasData(
                new Backpacks { CharacterId = 1, ItemId = 1, Amount = 2 },
                new Backpacks { CharacterId = 2, ItemId = 2, Amount = 1 },
                new Backpacks { CharacterId = 1, ItemId = 3, Amount = 4 }
            );

            modelBuilder.Entity<Titles>().HasData(
                new Titles { Id = 1, Name = "Title1" },
                new Titles { Id = 2, Name = "Title2" },
                new Titles { Id = 3, Name = "Title3" }
            );

            modelBuilder.Entity<Character_titles>().HasData(
                new Character_titles { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(1410, 6, 10) },
                new Character_titles { CharacterId = 1, TitleId = 2, AcquiredAt = new DateTime(2137, 6, 9) },
                new Character_titles { CharacterId = 2, TitleId = 3, AcquiredAt = new DateTime(2003, 7, 19) }
            );
        }
    }