using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class LikedAnimeService
    {
        private readonly ILikedAnimeRepository _likedAnimeRepository; 

        public LikedAnimeService(ILikedAnimeRepository likedAnimeRepository)
        {
            _likedAnimeRepository = likedAnimeRepository;
        }

        public List<LikedAnime> GetLikedAnimeByUser(int userId)
        {
            return _likedAnimeRepository.GetLikedAnimesForUser(userId);
        }
    }
}
