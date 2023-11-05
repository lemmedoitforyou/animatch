using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface ILikedAnimeRepository
    {
        List<LikedAnime> GetLikedAnimesForUser(int userId);
        void Add(LikedAnime liked);
        void Delete(LikedAnime liked);
    }
    public class LikedAnimeRepository : ILikedAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikedAnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<LikedAnime> GetLikedAnimesForUser(int userId)
        {
            return _context.LikedAnime.Where(l => l.UserId == userId).ToList();
        }

        public void Add(LikedAnime liked)
        {
            _context.LikedAnime.Add(liked);
            _context.SaveChanges();
        }

        public void Delete(LikedAnime liked)
        {
            _context.LikedAnime.Remove(liked);
            _context.SaveChanges();
        }
    }
}
