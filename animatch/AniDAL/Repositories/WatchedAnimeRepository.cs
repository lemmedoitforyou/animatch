using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IWatchedAnimeRepository: IGenericRepository<WatchedAnime>
    {
        List<Anime> GetWatchedAnimesForUser(int userId);
    }
    public class WatchedAnimeRepository : GenericRepository<WatchedAnime>, IWatchedAnimeRepository
    {
        public List<Anime> GetWatchedAnimesForUser(int userId)
        {
            var watchedAnime = _context.WatchedAnime
             .Where(a => a.UserId == userId)
             .Join(_context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
             .ToList();

            return watchedAnime;
        }
        public int GetLastId()
        {
            int lastId = _context.WatchedAnime.Max(w => w.Id);
            return lastId;
        }
    }
}
