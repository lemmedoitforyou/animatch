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
        List<DislikedAnime> GetDislikedAnimesForUser(int userId);
    }
    public class DislikedAnimeRepository : GenericRepository<DislikedAnime>, IDislikedAnimeRepository
    {
        public List<DislikedAnime> GetDislikedAnimesForUser(int userId)
        {
            return _context.DislikedAnime.Where(d => d.UserId == userId).ToList();
        }
    }
}
