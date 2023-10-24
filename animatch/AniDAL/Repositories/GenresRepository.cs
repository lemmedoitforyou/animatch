using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IGenresRepository
    {
        Genres GetById(int id);
        IEnumerable<Genres> GetAll();
        void Add(Genres genres);
        void Update(Genres genres);
        void Delete(Genres genres);
    }
    public class GenresRepository : IGenresRepository
    {
        private readonly ApplicationDbContext _context;

        public GenresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Genres GetById(int id)
        {
            return _context.Genres.FirstOrDefault(g => g.Id == id);
        }

        public IEnumerable<Genres> GetAll()
        {
            return _context.Genres.ToList();
        }

        public void Add(Genres genres)
        {
            _context.Genres.Add(genres);
            _context.SaveChanges();
        }

        public void Update(Genres genres)
        {
            _context.Genres.Update(genres);
            _context.SaveChanges();
        }

        public void Delete(Genres genres)
        {
            _context.Genres.Remove(genres);
            _context.SaveChanges();
        }
    }
}
