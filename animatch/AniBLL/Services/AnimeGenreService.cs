using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IAnimeGenreService
    {
        List<AnimeGenre> GetGenresForAnime(int animeId);
        List<AnimeGenre> GetAnimesForGenre(int genreId);
        void Insert(AnimeGenre animeGenres);
        void Delete(AnimeGenre animeGenres);
    }
    public class AnimeGenreService : IAnimeGenreService
    {
        private readonly IAnimeGenreRepository _animeGenreRepository; 

        public AnimeGenreService(IAnimeGenreRepository animeGenreRepository)
        {
            _animeGenreRepository = animeGenreRepository;
        }

        public List<AnimeGenre> GetGenresForAnime(int animeId)
        {
            return _animeGenreRepository.GetGenresForAnime(animeId);
        }

        public List<AnimeGenre> GetAnimesForGenre(int genreId)
        {
            return _animeGenreRepository.GetAnimesForGenre(genreId);
        }
        public void Insert(AnimeGenre animeGenres)
        {
            _animeGenreRepository.Insert(animeGenres);
        }
        public void Delete(AnimeGenre animeGenres)
        {
            _animeGenreRepository.Delete(animeGenres);
        }
    }
}
