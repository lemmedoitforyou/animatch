using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IAnimeGenreRepository : IGenericRepository<AnimeGenre>
    {
        List<string> GetGenresForAnime(int animeId);
        List<AnimeGenre> GetAnimesForGenre(int genreId);
    }
    public class AnimeGenreRepository : GenericRepository<AnimeGenre>, IAnimeGenreRepository
    {
        public List<string> GetGenresForAnime(int animeId)
        {
            var genreNames = _context.AnimeGenre
                .Where(ag => ag.AnimeId == animeId)
                .Join(_context.Genre, ag => ag.GenreId, g => g.Id, (ag, g) => g.Name)
                .ToList();

            return genreNames;
        }


        public List<AnimeGenre> GetAnimesForGenre(int genreId)
        {
            return _context.AnimeGenre.Where(ag => ag.GenreId == genreId).ToList();
        }
    }
}