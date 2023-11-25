using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface ILikedAnimeRepository: IGenericRepository<LikedAnime>
    {
        List<Anime> GetLikedAnimesForUser(int userId);
    }
    public class LikedAnimeRepository : GenericRepository<LikedAnime>, ILikedAnimeRepository
    {
        public List<Anime> GetLikedAnimesForUser(int userId)
        {
            var likedAnimes = _context.LikedAnime
           .Where(a => a.UserId == userId)
           .Join(_context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
           .ToList();
            return likedAnimes;
        }
    }
}
