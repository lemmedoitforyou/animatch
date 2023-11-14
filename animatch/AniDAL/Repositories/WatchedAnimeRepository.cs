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
        List<WatchedAnime> GetWatchedAnimesForUser(int userId);
    }
    public class WatchedAnimeRepository : GenericRepository<WatchedAnime>, IWatchedAnimeRepository
    {
        public List<WatchedAnime> GetWatchedAnimesForUser(int userId)
        {
            return _context.WatchedAnime.Where(w => w.UserId == userId).ToList();
        }
    }
}
