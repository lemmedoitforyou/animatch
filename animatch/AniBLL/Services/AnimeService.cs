using System;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IAnimeService
    {
        List<Anime> GetAll();
        Anime GetById(int animeId);
    }
    public class AnimeService: IAnimeService
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
    }
}
