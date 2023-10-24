using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IDislikedRepository
    {
        IEnumerable<Disliked> GetDislikedAnimesForUser(int userId);
        void Add(Disliked disliked);
        void Delete(Disliked disliked);
    }
    public class DislikedRepository : IDislikedRepository
    {
        private readonly ApplicationDbContext _context;

        public DislikedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Disliked> GetDislikedAnimesForUser(int userId)
        {
            return _context.Disliked.Where(d => d.UserId == userId).ToList();
        }

        public void Add(Disliked disliked)
        {
            _context.Disliked.Add(disliked);
            _context.SaveChanges();
        }

        public void Delete(Disliked disliked)
        {
            _context.Disliked.Remove(disliked);
            _context.SaveChanges();
        }
    }
}
