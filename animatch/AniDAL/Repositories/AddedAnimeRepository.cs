using AniDAL.DataBaseClasses;

namespace AniDAL.Repositories
{
    public interface IAddedAnimeRepository : IGenericRepository<AddedAnime>
    {
        List<Anime> GetAddedAnimesForUser(int userId);

        int CountUserWhoAddAnime(int animeID);
    }

    public class AddedAnimeRepository : GenericRepository<AddedAnime>, IAddedAnimeRepository
    {
        public List<Anime> GetAddedAnimesForUser(int userId)
        {
            var addedAnime = this._context.AddedAnime
            .Where(a => a.UserId == userId)
            .Join(this._context.Anime, a => a.AnimeId, g => g.Id, (a, g) => g)
            .ToList();

            return addedAnime;
        }

        public int CountUserWhoAddAnime(int animeID)
        {
            var userAdded = this._context.AddedAnime
            .Where(a => a.AnimeId == animeID).Count();
            return userAdded;
        }
    }
}
