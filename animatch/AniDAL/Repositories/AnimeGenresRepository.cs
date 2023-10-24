using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IAnimeGenresRepository
    {
        IEnumerable<AnimeGenres> GetGenresForAnime(int animeId);
        IEnumerable<AnimeGenres> GetAnimesForGenre(int genreId);
        void Add(AnimeGenres animeGenres);
        void Delete(AnimeGenres animeGenres);
    }
    public class AnimeGenresRepository : IAnimeGenresRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimeGenresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AnimeGenres> GetGenresForAnime(int animeId)
        {
            return _context.AnimeGenres.Where(ag => ag.AnimeId == animeId).ToList();
        }

        public IEnumerable<AnimeGenres> GetAnimesForGenre(int genreId)
        {
            return _context.AnimeGenres.Where(ag => ag.GenreId == genreId).ToList();
        }

        public void Add(AnimeGenres animeGenres)
        {
            _context.AnimeGenres.Add(animeGenres);
            _context.SaveChanges();
        }

        public void Delete(AnimeGenres animeGenres)
        {
            _context.AnimeGenres.Remove(animeGenres);
            _context.SaveChanges();
        }
    }
}
