using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface ILikedRepository
    {
        List<Liked> GetLikedAnimesForUser(int userId);
        void Add(Liked liked);
        void Delete(Liked liked);
    }
    public class LikedRepository : ILikedRepository
    {
        private readonly ApplicationDbContext _context;

        public LikedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Liked> GetLikedAnimesForUser(int userId)
        {
            return _context.Liked.Where(l => l.UserId == userId).ToList();
        }

        public void Add(Liked liked)
        {
            _context.Liked.Add(liked);
            _context.SaveChanges();
        }

        public void Delete(Liked liked)
        {
            _context.Liked.Remove(liked);
            _context.SaveChanges();
        }
    }
}
