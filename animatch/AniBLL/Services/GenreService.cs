using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;
using AniBLL.Models;

namespace AniBLL.Services
{
    public interface IGenreService
    {
        GenreModel GetById(int id);
        List<GenreModel> GetAll();
        void Insert(GenreModel genres);
        void Update(GenreModel genres);
        void Delete(GenreModel genres);
    }
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository; 

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public List<GenreModel> GetAll()
        {
            var genres = _genreRepository.GetAll();

            var genreModels = genres.Select(genre => new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name
            }).ToList();

            return genreModels;
        }

        public GenreModel GetById(int genreId)
        {
            return (GenreModel)_genreRepository.GetById(genreId);
        }
        public void Insert(GenreModel genres)
        {
            _genreRepository.Insert(genres);
        }
        public void Update(GenreModel genres)
        {
            _genreRepository.Update(genres);
        }
        public void Delete(GenreModel genres)
        {
            _genreRepository.Delete(genres);
        }
    }
}
