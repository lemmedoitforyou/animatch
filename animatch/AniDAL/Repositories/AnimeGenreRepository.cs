using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IAnimeGenreRepository : IGenericRepository<AnimeGenre>
    {
        List<string> GetGenresForAnime(int animeId);

        List<Anime> GetAnimesForGenre(int genreId);
    }

    public class AnimeGenreRepository : GenericRepository<AnimeGenre>, IAnimeGenreRepository
    {
        public List<string> GetGenresForAnime(int animeId)
        {
            var genreNames = this._context.AnimeGenre
                .Where(ag => ag.AnimeId == animeId)
                .Join(this._context.Genre, ag => ag.GenreId, g => g.Id, (ag, g) => g.Name)
                .ToList();

            return genreNames;
        }

        public List<Anime> GetAnimesForGenre(int genreId)
        {
            return this._context.AnimeGenre
                .Where(ag => ag.GenreId == genreId)
                .Join(this._context.Anime, ag => ag.AnimeId, a => a.Id, (ag, a) => a)
                .ToList();
        }
    }
}
