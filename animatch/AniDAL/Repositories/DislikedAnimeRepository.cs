using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IDislikedAnimeRepository: IGenericRepository<DislikedAnime>
    {
        List<Anime> GetDislikedAnimesForUser(int userId);
    }
    public class DislikedAnimeRepository : GenericRepository<DislikedAnime>, IDislikedAnimeRepository
    {
        public List<Anime> GetDislikedAnimesForUser(int userId)
        {
            var dislikedAnime = _context.DislikedAnime
            .Where(a => a.UserId == userId)
            .Join(_context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
            .ToList();

            return dislikedAnime;
        }
        public int GetLastId()
        {

            int lastId = _context.DislikedAnime.Max(w => w.Id);
            return lastId;
        }
    }
}
