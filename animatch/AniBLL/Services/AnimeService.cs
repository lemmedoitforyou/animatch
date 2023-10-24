using System;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class AnimeService
    {
        private readonly IAnimeRepository _animeRepository;

        public AnimeService(IAnimeRepository animeRepository)
        {
            _animeRepository = animeRepository;
        }

        public List<Anime> GetAllAnime()
        {
            return _animeRepository.GetAll();
        }
        public Anime GetAnimeById(int animeId)
        {
            return _animeRepository.GetById(animeId);
        }
    }
}
