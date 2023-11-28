using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IWatchedAnimeService
    {
        List<Anime> GetWatchedAnimesForUser(int userId);
        void Insert(WatchedAnime watched);
        void Delete(WatchedAnime watched);
    }
    public class WatchedAnimeService : IWatchedAnimeService
    {
        private readonly IWatchedAnimeRepository _watchedAnimeRepository; 

        public WatchedAnimeService(IWatchedAnimeRepository watchedAnimeRepository)
        {
            _watchedAnimeRepository = watchedAnimeRepository;
        }

        public List<Anime> GetWatchedAnimesForUser(int userId)
        {
            return _watchedAnimeRepository.GetWatchedAnimesForUser(userId);
        }
        public void Insert(WatchedAnime watched)
        {
            _watchedAnimeRepository.Insert(watched);
        }

        public void Delete(WatchedAnime watched)
        {
            _watchedAnimeRepository.Delete(watched);
        }
    }
}
