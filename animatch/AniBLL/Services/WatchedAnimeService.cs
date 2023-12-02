using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;
using AniBLL.Models;

namespace AniBLL.Services
{
    public interface IWatchedAnimeService
    {
        List<AnimeModel> GetWatchedAnimesForUser(int userId);
        void Insert(WatchedAnimeModel watched);
        void Delete(WatchedAnimeModel watched);
        int GetLastId();
    }
    public class WatchedAnimeService : IWatchedAnimeService
    {
        private readonly IWatchedAnimeRepository _watchedAnimeRepository; 

        public WatchedAnimeService(IWatchedAnimeRepository watchedAnimeRepository)
        {
            _watchedAnimeRepository = watchedAnimeRepository;
        }

        public List<AnimeModel> GetWatchedAnimesForUser(int userId)
        {
            List<Anime> watchedAnimesFromRepository =
                _watchedAnimeRepository.GetWatchedAnimesForUser(userId);

            List<AnimeModel> likedAnimes = watchedAnimesFromRepository
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            return likedAnimes;
        }
        public void Insert(WatchedAnimeModel watched)
        {
            _watchedAnimeRepository.Insert(watched);
        }

        public void Delete(WatchedAnimeModel watched)
        {
            _watchedAnimeRepository.Delete(watched);
        }
        public int GetLastId()
        {
            return _watchedAnimeRepository.GetLastId();
        }
    }
}
