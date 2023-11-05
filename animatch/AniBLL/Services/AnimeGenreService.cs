using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class AnimeGenreService
    {
        private readonly IAnimeGenreRepository _animeGenreRepository; 

        public AnimeGenreService(IAnimeGenreRepository animeGenreRepository)
        {
            _animeGenreRepository = animeGenreRepository;
        }

        public List<AnimeGenre> GetGenreByAnime(int animeId)
        {
            return _animeGenreRepository.GetGenresForAnime(animeId);
        }

        public List<AnimeGenre> GetAnimeByGenre(int genreId)
        {
            return _animeGenreRepository.GetAnimesForGenre(genreId);
        }
    }
}
