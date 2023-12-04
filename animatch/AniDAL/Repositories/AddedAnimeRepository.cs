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
        List<Anime> GetAddedAnimesForUser(int userId);
        int CountUserWhoAddAnime(int animeID);
    }
    public class AddedAnimeRepository : GenericRepository<AddedAnime>, IAddedAnimeRepository
    {
        public List<Anime> GetAddedAnimesForUser(int userId)
        {
            var addedAnime = _context.AddedAnime
            .Where(a => a.UserId == userId)
            .Join(_context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
            .ToList();

            return addedAnime;
        }  
        public int CountUserWhoAddAnime(int animeID)
        {
            var userAdded = _context.AddedAnime
            .Where(a => a.AnimeId == animeID).Count();
            return userAdded;
        }
    }

}
