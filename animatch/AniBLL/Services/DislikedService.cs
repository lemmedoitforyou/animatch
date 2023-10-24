using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class DislikedService
    {
        private readonly IDislikedRepository _dislikedRepository; // Підключення до репозиторію "disliked"

        public DislikedService(IDislikedRepository dislikedRepository)
        {
            _dislikedRepository = dislikedRepository;
        }

        public List<Disliked> GetDislikedAnimeByUser(int userId)
        {
            return _dislikedRepository.GetDislikedAnimesForUser(userId);
        }

        
        // Інші методи для роботи з відзначенням аніме як "не подобається" користувачам
    }
}
