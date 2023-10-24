using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IAnimeRepository
    {
        Anime GetById(int id);
        List<Anime> GetAll();
        void Add(Anime anime);
        void Update(Anime anime);
        void Delete(Anime anime);
    }
    public class AnimeRepository : IAnimeRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Anime GetById(int id)
        {
            return _context.Anime.FirstOrDefault(a => a.Id == id);
        }

        public List<Anime> GetAll()
        {
            return _context.Anime.ToList();
        }

        public void Add(Anime anime)
        {
            _context.Anime.Add(anime);
            _context.SaveChanges();
        }

        public void Update(Anime anime)
        {
            _context.Anime.Update(anime);
            _context.SaveChanges();
        }

        public void Delete(Anime anime)
        {
            _context.Anime.Remove(anime);
            _context.SaveChanges();
        }
    }
}
