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
        List<int> GetLikedAnimesForUser(int userId);
    }
    public class LikedAnimeRepository : GenericRepository<LikedAnime>, ILikedAnimeRepository
    {
        public List<int> GetLikedAnimesForUser(int userId)
        {
            return _context.LikedAnime
           .Where(l => l.UserId == userId)
           .Select(l => l.AnimeId)
           .ToList();
        }
    }
}
