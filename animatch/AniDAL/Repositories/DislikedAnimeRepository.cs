using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IDislikedAnimeRepository : IGenericRepository<DislikedAnime>
    {
        List<Anime> GetDislikedAnimesForUser(int userId);
    }

    public class DislikedAnimeRepository : GenericRepository<DislikedAnime>, IDislikedAnimeRepository
    {
        public List<Anime> GetDislikedAnimesForUser(int userId)
        {
            var dislikedAnime = this._context.DislikedAnime
            .Where(a => a.UserId == userId)
            .Join(this._context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
            .ToList();

            return dislikedAnime;
        }

        public int GetLastId()
        {
            int lastId = this._context.DislikedAnime.Max(w => w.Id);
            return lastId;
        }
    }
}
