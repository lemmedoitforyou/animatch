using AniBLL.Models;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IAnimeService
    {
        AnimeModel GetById(int id);

        List<AnimeModel> GetAll();

        void Insert(AnimeModel anime);

        void Update(AnimeModel anime);

        void Delete(int anime);
    }

    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            this._animeRepository = animeRepository;
        }

        public List<AnimeModel> GetAll()
        {
            List<Anime> animeRepository = this._animeRepository.GetAll().ToList();
            List<AnimeModel> anime1 = animeRepository
                                .Select(anime => new AnimeModel
                                {
                                    Id = anime.Id,
                                    Name = anime.Name,
                                    Text = anime.Text,
                                    Imdbrate = anime.Imdbrate,
                                    Photo = anime.Photo,
                                    Year = anime.Year,
                                })
                                .ToList();

            return anime1;
        }

        public AnimeModel GetById(int animeId)
        {
            var anime = this._animeRepository.GetById(animeId);

            if (anime == null)
            {
                return null;
            }

            var animeModel = new AnimeModel
            {
                Id = anime.Id,
                Name = anime.Name,
                Text = anime.Text,
                Imdbrate = anime.Imdbrate,
                Photo = anime.Photo,
                Year = anime.Year,
            };

            return animeModel;
        }

        public void Insert(AnimeModel anime)
        {
            this._animeRepository.Insert(anime);
        }

        public void Update(AnimeModel anime)
        {
            this._animeRepository.Update(anime);
        }

        public void Delete(int anime)
        {
            this._animeRepository.Delete(anime);
        }
    }
}
