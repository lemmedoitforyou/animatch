using System;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IAnimeService
    {
        Anime GetById(int id);
        List<Anime> GetAll();
        void Insert(Anime anime);
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
            return _animeRepository.GetAll().ToList();
        }
        
        public Anime GetById(int animeId)
        {
            return _animeRepository.GetById(animeId);
        }

        public void Insert(Anime anime)
        {
            _animeRepository.Insert(anime);
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
