using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IAddedAnimeRepository
    {
        List<AddedAnime> GetAddedAnimesForUser(int userId);
        void Add(AddedAnime added);
        void Delete(AddedAnime added);
    }
    public class AddedAnimeRepository : IAddedAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public AddedAnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AddedAnime> GetAddedAnimesForUser(int userId)
        {
            return _context.AddedAnime.Where(a => a.UserId == userId).ToList();
        }

        public void Add(AddedAnime added)
        {
            _context.AddedAnime.Add(added);
            _context.SaveChanges();
        }

        public void Delete(AddedAnime added)
        {
            _context.AddedAnime.Remove(added);
            _context.SaveChanges();
        }
    }

}
