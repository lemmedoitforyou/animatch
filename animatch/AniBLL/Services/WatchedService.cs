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
        private readonly IWatchedRepository _watchedRepository; // Підключення до репозиторію "watched"

        public WatchedService(IWatchedRepository watchedRepository)
        {
            _watchedRepository = watchedRepository;
        }

        public List<Watched> GetWatchedAnimeByUser(int userId)
        {
            return _watchedRepository.GetWatchedAnimesForUser(userId);
        }

        

        // Інші методи для роботи з переглянутим аніме користувачами
    }
}
