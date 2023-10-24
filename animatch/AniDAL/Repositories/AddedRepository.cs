using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IAddedRepository
    {
        List<Added> GetAddedAnimesForUser(int userId);
        void Add(Added added);
        void Delete(Added added);
    }
    public class AddedRepository : IAddedRepository
    {
        private readonly ApplicationDbContext _context;

        public AddedRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Added> GetAddedAnimesForUser(int userId)
        {
            return _context.Added.Where(a => a.UserId == userId).ToList();
        }

        public void Add(Added added)
        {
            _context.Added.Add(added);
            _context.SaveChanges();
        }

        public void Delete(Added added)
        {
            _context.Added.Remove(added);
            _context.SaveChanges();
        }
    }

}
