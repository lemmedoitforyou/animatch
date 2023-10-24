using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class GenresService
    {
        private readonly IGenresRepository _genresRepository; // Підключення до репозиторію жанрів

        public GenresService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        public List<Genres> GetAllGenres()
        {
            return _genresRepository.GetAll();
        }

        public Genres GetGenreById(int genreId)
        {
            return _genresRepository.GetById(genreId);
        }

        // Інші методи для роботи з жанрами
    }
}
