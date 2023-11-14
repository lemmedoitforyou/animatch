using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IAnimeGenreRepository: IGenericRepository<AnimeGenre>
    {
        List<AnimeGenre> GetGenresForAnime(int animeId);
        List<AnimeGenre> GetAnimesForGenre(int genreId);
    }
    public class AnimeGenreRepository : GenericRepository<AnimeGenre>, IAnimeGenreRepository
    {
        public List<AnimeGenre> GetGenresForAnime(int animeId)
        {
            return _context.AnimeGenre.Where(ag => ag.AnimeId == animeId).ToList();
        }

        public List<AnimeGenre> GetAnimesForGenre(int genreId)
        {
            return _context.AnimeGenre.Where(ag => ag.GenreId == genreId).ToList();
        }
    }
}
