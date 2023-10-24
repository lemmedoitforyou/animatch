using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class LikedService
    {
        private readonly ILikedRepository _likedRepository; // Підключення до репозиторію "liked"

        public LikedService(ILikedRepository likedRepository)
        {
            _likedRepository = likedRepository;
        }

        public List<Liked> GetLikedAnimeByUser(int userId)
        {
            return _likedRepository.GetLikedAnimesForUser(userId);
        }

        

        // Інші методи для роботи з відзначенням аніме як "подобається" користувачам
    }
}
