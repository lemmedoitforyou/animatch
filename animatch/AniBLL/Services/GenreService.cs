using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IGenreService
    {
        Genre GetById(int id);
        List<Genre> GetAll();
        void Insert(Genre genres);
        void Update(Genre genres);
        void Delete(Genre genres);
    }
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository; 

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<Genre> GetAll()
        {
            return _genreRepository.GetAll().ToList();
        }

        public Genre GetById(int genreId)
        {
            return _genreRepository.GetById(genreId);
        }
        public void Insert(Genre genres)
        {
            _genreRepository.Insert(genres);
        }
        public void Update(Genre genres)
        {
            _genreRepository.Update(genres);
        }
        public void Delete(Genre genres)
        {
            _genreRepository.Delete(genres);
        }
    }
}
