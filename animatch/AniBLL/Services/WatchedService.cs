using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class WatchedService
    {
        private readonly IWatchedAnimeRepository _watchedAnimeRepository; 

        public WatchedService(IWatchedAnimeRepository watchedAnimeRepository)
        {
            _watchedAnimeRepository = watchedAnimeRepository;
        }

        public List<WatchedAnime> GetWatchedAnimeByUser(int userId)
        {
            return _watchedAnimeRepository.GetWatchedAnimesForUser(userId);
        }
    }
}
