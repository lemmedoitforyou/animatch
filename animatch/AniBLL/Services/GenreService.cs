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
        void Add(Genre genres);
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
            return _genreRepository.GetAll();
        }

        public Genre GetById(int genreId)
        {
            return _genreRepository.GetById(genreId);
        }
        public void Add(Genre genres)
        {
            _genreRepository.Add(genres);
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
