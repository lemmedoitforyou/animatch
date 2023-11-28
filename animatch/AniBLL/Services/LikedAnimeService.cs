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
    public interface ILikedAnimeService
    {
        List<AnimeModel> GetLikedAnimesForUser(int userId);
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

        public List<AnimeModel> GetLikedAnimesForUser(int userId)
        {
            return (AnimeModel)_likedAnimeRepository.GetLikedAnimesForUser(userId);
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
