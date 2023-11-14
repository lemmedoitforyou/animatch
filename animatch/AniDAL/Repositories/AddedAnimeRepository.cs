using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AniDAL.Repositories
{
    public interface IAddedAnimeRepository: IGenericRepository<AddedAnime>
    {
        List<AddedAnime> GetAddedAnimesForUser(int userId);
    }
    public class AddedAnimeRepository : GenericRepository<AddedAnime>, IAddedAnimeRepository
    {
        public List<AddedAnime> GetAddedAnimesForUser(int userId)
        {
            return _context.AddedAnime.Where(a => a.UserId == userId).ToList();
        }
    }

}
