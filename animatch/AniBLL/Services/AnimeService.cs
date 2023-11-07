using System;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IAnimeService
    {
        Anime GetById(int id);
        List<Anime> GetAll();
        void Add(Anime anime);
        void Update(Anime anime);
        void Delete(Anime anime);
    }
    public class AnimeService : IAnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public List<Anime> GetAll()
        {
            return _animeRepository.GetAll();
        }

        public Anime GetById(int animeId)
        {
            return _animeRepository.GetById(animeId);
        }

        public void Add(Anime anime)
        {
            _animeRepository.Add(anime);
        }
        public void Update(Anime anime)
        {
            _animeRepository.Update(anime);
        }
        public void Delete(Anime anime)
        {
            _animeRepository.Delete(anime);
        }
    }
}
