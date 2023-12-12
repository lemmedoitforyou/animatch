using AniBLL.Models;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IAnimeGenreService
    {
        List<string> GetGenresForAnime(int animeId);

        List<AnimeModel> GetAnimesForGenre(int genreId);

        void Insert(AnimeGenreModel animeGenres);

        void Delete(int animeGenres);
    }

    public class AnimeGenreService : IAnimeGenreService
    {
        private readonly IAnimeGenreRepository _animeGenreRepository;

        public AnimeGenreService(IAnimeGenreRepository animeGenreRepository)
        {
            this._animeGenreRepository = animeGenreRepository;
        }

        public List<string> GetGenresForAnime(int animeId)
        {
            return this._animeGenreRepository.GetGenresForAnime(animeId);
        }

        public List<AnimeModel> GetAnimesForGenre(int genreId)
        {
            List<Anime> animeGenreRepository = this._animeGenreRepository.GetAnimesForGenre(genreId);
            List<AnimeModel> animeForGenre = animeGenreRepository.Select(anime => new AnimeModel
            {
                Id = anime.Id,
                Name = anime.Name,
                Text = anime.Text,
                Imdbrate = anime.Imdbrate,
                Photo = anime.Photo,
                Year = anime.Year,
            }).ToList();

            return animeForGenre;
        }

        public void Insert(AnimeGenreModel animeGenres)
        {
            this._animeGenreRepository.Insert(animeGenres);
        }

        public void Delete(int animeGenres)
        {
            this._animeGenreRepository.Delete(animeGenres);
        }
    }
}
