using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IGenreRepository
    {
        Genre GetById(int id);
        List<Genre> GetAll();
        void Add(Genre genres);
        void Update(Genre genres);
        void Delete(Genre genres);
    }
    public class GenreRepository : IGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public GenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Genre GetById(int id)
        {
            return _context.Genre.FirstOrDefault(g => g.Id == id);
        }

        public List<Genre> GetAll()
        {
            return _context.Genre.ToList();
        }

        public void Add(Genre genres)
        {
            _context.Genre.Add(genres);
            _context.SaveChanges();
        }

        public void Update(Genre genres)
        {
            _context.Genre.Update(genres);
            _context.SaveChanges();
        }

        public void Delete(Genre genres)
        {
            _context.Genre.Remove(genres);
            _context.SaveChanges();
        }
    }
}
