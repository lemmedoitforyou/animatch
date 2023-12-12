using AniBLL.Models;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IWatchedAnimeService
    {
        List<AnimeModel> GetWatchedAnimesForUser(int userId);

        void Insert(WatchedAnimeModel watched);

        void Delete(int watched);

        int GetLastId();
    }

    public class WatchedAnimeService : IWatchedAnimeService
    {
        private readonly IWatchedAnimeRepository _watchedAnimeRepository;

        public WatchedAnimeService(IWatchedAnimeRepository watchedAnimeRepository)
        {
            this._watchedAnimeRepository = watchedAnimeRepository;
        }

        public List<AnimeModel> GetWatchedAnimesForUser(int userId)
        {
            List<Anime> watchedAnimesFromRepository =
                this._watchedAnimeRepository.GetWatchedAnimesForUser(userId);

            List<AnimeModel> likedAnimes = watchedAnimesFromRepository
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year,
                })
                .ToList();

            return likedAnimes;
        }

        public void Insert(WatchedAnimeModel watched)
        {
            this._watchedAnimeRepository.Insert(watched);
        }

        public void Delete(int watched)
        {
            this._watchedAnimeRepository.Delete(watched);
        }

        public int GetLastId()
        {
            return this._watchedAnimeRepository.GetLastId();
        }
    }
}
