using AniBLL.Models;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IAddedAnimeService
    {
        List<AnimeModel> GetAddedAnimesForUser(int userId);

        void Add(AddedAnimeModel added);

        void Delete(int added);

        int CountUserWhoAddAnime(int animeID);
    }

    public class AddedAnimeService : IAddedAnimeService
    {
        private readonly IAddedAnimeRepository _addedAnimeRepository;

        public AddedAnimeService(IAddedAnimeRepository addedAnimeRepository)
        {
            this._addedAnimeRepository = addedAnimeRepository;
        }

        public List<AnimeModel> GetAddedAnimesForUser(int userId)
        {
            List<AniDAL.DataBaseClasses.Anime> addedAnimeRepository = this._addedAnimeRepository.GetAddedAnimesForUser(userId);

            // Перетворення об'єктів Anime на об'єкти AnimeModel
            List<AnimeModel> addedAnime = addedAnimeRepository
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

            return addedAnime;
        }

        public void Add(AddedAnimeModel added)
        {
            this._addedAnimeRepository.Insert(added);
        }

        public void Delete(int added)
        {
            this._addedAnimeRepository.Delete(added);
        }

        public int CountUserWhoAddAnime(int animeID)
        {
            return this._addedAnimeRepository.CountUserWhoAddAnime(animeID);
        }
    }
}
