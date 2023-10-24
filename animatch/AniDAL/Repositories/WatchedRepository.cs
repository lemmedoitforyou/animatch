using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IWatchedRepository
    {
        List<Watched> GetWatchedAnimesForUser(int userId);
        void Add(Watched watched);
        void Delete(Watched watched);
    }
    public class WatchedRepository : IWatchedRepository
    {
        private readonly ApplicationDbContext _context;

        public WatchedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Watched> GetWatchedAnimesForUser(int userId)
        {
            return _context.Watched.Where(w => w.UserId == userId).ToList();
        }

        public void Add(Watched watched)
        {
            _context.Watched.Add(watched);
            _context.SaveChanges();
        }

        public void Delete(Watched watched)
        {
            _context.Watched.Remove(watched);
            _context.SaveChanges();
        }
    }
}
