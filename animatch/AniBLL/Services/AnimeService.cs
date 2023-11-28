using System;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;
using AniBLL.Models;

namespace AniBLL.Services
{
    public interface IAnimeService
    {
        AnimeModel GetById(int id);
        List<AnimeModel> GetAll();
        void Insert(AnimeModel anime);
        void Update(AnimeModel anime);
        void Delete(AnimeModel anime);
    }
    public class AnimeService : IAnimeService

    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public List<AnimeModel> GetAll()
        {
            List<Anime> animeRepository = _animeRepository.GetAll().ToList();
            List<AnimeModel> Anime = animeRepository
                                .Select(anime => new AnimeModel
                                {
                                    Id = anime.Id,
                                    Name = anime.Name,
                                    Text = anime.Text,
                                    Imdbrate = anime.Imdbrate,
                                    Photo = anime.Photo,
                                    Year = anime.Year
                                })
                                .ToList();

            return Anime;
        }

        public AnimeModel GetById(int animeId)
        {
            var anime = _animeRepository.GetById(animeId);

            // Перевірка на null, якщо аніме не знайдено
            if (anime == null)
            {
                return null; // або можна кинути виняток чи повернути якусь іншу логіку за замовчуванням
            }

            // Створення екземпляра AnimeModel на основі об'єкта з репозиторію
            var animeModel = new AnimeModel
            {
                Id = anime.Id,
                Name = anime.Name,
                Text = anime.Text,
                Imdbrate = anime.Imdbrate,
                Photo = anime.Photo,
                Year = anime.Year
            };

            return animeModel;
        }


        public void Insert(AnimeModel anime)
        {
            _animeRepository.Insert(anime);
        }
        public void Update(AnimeModel anime)
        {
            _animeRepository.Update(anime);
        }
        public void Delete(AnimeModel anime)
        {
            _animeRepository.Delete(anime);
        }
    }
}
