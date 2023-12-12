using AniBLL.Models;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface ILikedAnimeService
    {
        List<AnimeModel> GetLikedAnimesForUser(int userId);

        void Insert(LikedAnimeModel liked);

        void Delete(int liked);

        int GetLastUserId();
    }

    public class LikedAnimeService : ILikedAnimeService
    {
        private readonly ILikedAnimeRepository _likedAnimeRepository;

        public LikedAnimeService(ILikedAnimeRepository likedAnimeRepository)
        {
            this._likedAnimeRepository = likedAnimeRepository;
        }

        public List<AnimeModel> GetLikedAnimesForUser(int userId)
        {
            List<AniDAL.DataBaseClasses.Anime> likedAnimesFromRepository =
                this._likedAnimeRepository.GetLikedAnimesForUser(userId);

            List<AnimeModel> likedAnimes = likedAnimesFromRepository
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

        public int GetLastUserId()
        {
            return this._likedAnimeRepository.GetLastUserId();
        }

        public void Insert(LikedAnimeModel liked)
        {
            this._likedAnimeRepository.Insert(liked);
        }

        public void Delete(int liked)
        {
            this._likedAnimeRepository.Delete(liked);
        }
    }
}
