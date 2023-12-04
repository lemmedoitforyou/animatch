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
        int GetLastUserId();
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
        public int GetLastUserId()
        {
            int lastUserId = _context.LikedAnime.Max(u => u.Id);
            return lastUserId;
        }
        public int CountUserLikedAnime(int animeID)
        {
            var likedUser = _context.LikedAnime
           .Where(a => a.AnimeId == animeID)
           .Count();
            return likedUser;
        }
    }

}
