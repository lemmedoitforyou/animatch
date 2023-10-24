using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class AddedService
    {
        private readonly IAddedRepository _addedRepository; // Підключення до репозиторію "added"

        public AddedService(IAddedRepository addedRepository)
        {
            _addedRepository = addedRepository;
        }

        public List<Added> GetAnimeAddedByUser(int userId)
        {
            return _addedRepository.GetAddedAnimesForUser(userId);
        }

        
        // Інші методи для роботи з додаванням аніме користувачам
    }
}
