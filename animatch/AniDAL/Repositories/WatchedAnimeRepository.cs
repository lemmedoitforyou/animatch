using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IWatchedAnimeRepository
    {
        List<WatchedAnime> GetWatchedAnimesForUser(int userId);
        void Add(WatchedAnime watched);
        void Delete(WatchedAnime watched);
    }
    public class WatchedAnimeRepository : IWatchedAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchedAnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<WatchedAnime> GetWatchedAnimesForUser(int userId)
        {
            return _context.WatchedAnime.Where(w => w.UserId == userId).ToList();
        }

        public void Add(WatchedAnime watched)
        {
            _context.WatchedAnime.Add(watched);
            _context.SaveChanges();
        }

        public void Delete(WatchedAnime watched)
        {
            _context.WatchedAnime.Remove(watched);
            _context.SaveChanges();
        }
    }
}
