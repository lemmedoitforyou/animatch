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
        List<LikedAnime> GetLikedAnimesForUser(int userId);
        void Add(LikedAnime liked);
        void Delete(LikedAnime liked);
    }
    public class LikedAnimeService : ILikedAnimeService
    {
        private readonly ILikedAnimeRepository _likedAnimeRepository; 

        public LikedAnimeService(ILikedAnimeRepository likedAnimeRepository)
        {
            _likedAnimeRepository = likedAnimeRepository;
        }

        public List<LikedAnime> GetLikedAnimesForUser(int userId)
        {
            return _likedAnimeRepository.GetLikedAnimesForUser(userId);
        }
        public void Add(LikedAnime liked)
        {
            _likedAnimeRepository.Add(liked);
        }
        public void Delete(LikedAnime liked)
        {
            _likedAnimeRepository.Delete(liked);
        }
    }
}
