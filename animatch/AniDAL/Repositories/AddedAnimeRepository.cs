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
        List<Anime> GetAddedAnimesForUser(int userId);
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

        public List<Anime> GetAddedAnimesForUser(int userId)
        {
            var addedAnimeIds = _context.AddedAnime.Where(a => a.UserId == userId).Select(a => a.AnimeId).ToList();
            return _context.Anime.Where(anime => addedAnimeIds.Contains(anime.Id)).ToList();
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
