using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IAnimeGenreRepository
    {
        List<AnimeGenre> GetGenresForAnime(int animeId);
        List<AnimeGenre> GetAnimesForGenre(int genreId);
        void Add(AnimeGenre animeGenres);
        void Delete(AnimeGenre animeGenres);
    }
    public class AnimeGenreRepository : IAnimeGenreRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimeGenreRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<AnimeGenre> GetGenresForAnime(int animeId)
        {
            return _context.AnimeGenre.Where(ag => ag.AnimeId == animeId).ToList();
        }

        public List<AnimeGenre> GetAnimesForGenre(int genreId)
        {
            return _context.AnimeGenre.Where(ag => ag.GenreId == genreId).ToList();
        }

        public void Add(AnimeGenre animeGenres)
        {
            _context.AnimeGenre.Add(animeGenres);
            _context.SaveChanges();
        }

        public void Delete(AnimeGenre animeGenres)
        {
            _context.AnimeGenre.Remove(animeGenres);
            _context.SaveChanges();
        }
    }
}
