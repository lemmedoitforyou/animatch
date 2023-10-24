using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class AnimeGenresService
    {
        private readonly IAnimeGenresRepository _animeGenresRepository; // Підключення до репозиторію "animeGenres"

        public AnimeGenresService(IAnimeGenresRepository animeGenresRepository)
        {
            _animeGenresRepository = animeGenresRepository;
        }

        public List<AnimeGenres> GetGenresByAnime(int animeId)
        {
            return _animeGenresRepository.GetGenresForAnime(animeId);
        }

        public List<AnimeGenres> GetAnimeByGenre(int genreId)
        {
            return _animeGenresRepository.GetAnimesForGenre(genreId);
        }

        
        // Інші методи для роботи з жанрами аніме
    }
}
