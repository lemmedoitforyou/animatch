using AniBLL.Models;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IDislikedAnimeService
    {
        List<AnimeModel> GetDislikedAnimesForUser(int userId);

        void Insert(DislikedAnimeModel disliked);

        void Delete(int disliked);

        int GetLastId();
    }

    public class DislikedAnimeService : IDislikedAnimeService
    {
        private readonly IDislikedAnimeRepository _dislikedAnimeRepository;

        public DislikedAnimeService(IDislikedAnimeRepository dislikedAnimeRepository)
        {
            this._dislikedAnimeRepository = dislikedAnimeRepository;
        }

        public List<AnimeModel> GetDislikedAnimesForUser(int userId)
        {
            List<AniDAL.DataBaseClasses.Anime> dislikedAnimeRepository =
                this._dislikedAnimeRepository.GetDislikedAnimesForUser(userId);

            List<AnimeModel> dislikedAnime = dislikedAnimeRepository
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

            return dislikedAnime;
        }

        public void Insert(DislikedAnimeModel disliked)
        {
            this._dislikedAnimeRepository.Insert(disliked);
        }

        public void Delete(int disliked)
        {
            this._dislikedAnimeRepository.Delete(disliked);
        }

        public int GetLastId()
        {
            return this._dislikedAnimeRepository.GetLastId();
        }
    }
}
