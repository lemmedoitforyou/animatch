using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface ILikedAnimeService
    {
        List<int> GetLikedAnimesForUser(int userId);
        void Insert(LikedAnime liked);
        void Delete(LikedAnime liked);
    }
    public class LikedAnimeService : ILikedAnimeService
    {
        private readonly ILikedAnimeRepository _likedAnimeRepository; 

        public LikedAnimeService(ILikedAnimeRepository likedAnimeRepository)
        {
            _likedAnimeRepository = likedAnimeRepository;
        }

        public List<int> GetLikedAnimesForUser(int userId)
        {
            return _likedAnimeRepository.GetLikedAnimesForUser(userId);
        }
        public void Insert(LikedAnime liked)
        {
            _likedAnimeRepository.Insert(liked);
        }
        public void Delete(LikedAnime liked)
        {
            _likedAnimeRepository.Delete(liked);
        }
    }
}
