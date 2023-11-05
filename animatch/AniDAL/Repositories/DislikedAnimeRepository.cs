using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IDislikedAnimeRepository
    {
        List<DislikedAnime> GetDislikedAnimesForUser(int userId);
        void Add(DislikedAnime disliked);
        void Delete(DislikedAnime disliked);
    }
    public class DislikedAnimeRepository : IDislikedAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public DislikedAnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<DislikedAnime> GetDislikedAnimesForUser(int userId)
        {
            return _context.DislikedAnime.Where(d => d.UserId == userId).ToList();
        }

        public void Add(DislikedAnime disliked)
        {
            _context.DislikedAnime.Add(disliked);
            _context.SaveChanges();
        }

        public void Delete(DislikedAnime disliked)
        {
            _context.DislikedAnime.Remove(disliked);
            _context.SaveChanges();
        }
    }
}
