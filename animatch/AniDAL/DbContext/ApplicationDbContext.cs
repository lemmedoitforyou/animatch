using AniDAL.DataBaseClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AniDAL.DbContext
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Genre> Genre { get; set; }
        public DbSet<AnimeGenre> AnimeGenre { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<AddedAnime> AddedAnime { get; set; }
        public DbSet<DislikedAnime> DislikedAnime { get; set; }
        public DbSet<LikedAnime> LikedAnime { get; set; }
        public DbSet<WatchedAnime> WatchedAnime { get; set; }
        public DbSet<Review> Review { get; set; }


        public ApplicationDbContext()
            : base()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;User Id=postgres;Password=13112004k;Database=animatch;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
