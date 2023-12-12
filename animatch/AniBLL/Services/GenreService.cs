using AniBLL.Models;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IGenreService
    {
        GenreModel GetById(int id);

        List<GenreModel> GetAll();

        void Insert(GenreModel genres);

        void Update(GenreModel genres);

        void Delete(int genres);
    }

    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public List<GenreModel> GetAll()
        {
            var genres = this._genreRepository.GetAll();

            var genreModels = genres.Select(genre => new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name,
            }).ToList();

            return genreModels;
        }

        public GenreModel GetById(int genreId)
        {
            var genre = this._genreRepository.GetById(genreId);
            var genreModel = new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name,
            };

            return genreModel;
        }

        public void Insert(GenreModel genres)
        {
            this._genreRepository.Insert(genres);
        }

        public void Update(GenreModel genres)
        {
            this._genreRepository.Update(genres);
        }

        public void Delete(int genres)
        {
            this._genreRepository.Delete(genres);
        }
    }
}
