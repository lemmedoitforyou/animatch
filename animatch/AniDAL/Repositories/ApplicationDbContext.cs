using AniDAL.DataBaseClasses;
using Microsoft.EntityFrameworkCore;

namespace AniDAL.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Anime> Anime { get; set; }
        public DbSet<Genres> Genres { get; set; }
        public DbSet<AnimeGenres> AnimeGenres { get; set; }
        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Added> Added { get; set; }
        public DbSet<Disliked> Disliked { get; set; }
        public DbSet<Liked> Liked { get; set; }
        public DbSet<Watched> Watched { get; set; }
        public DbSet<Review> Review { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
