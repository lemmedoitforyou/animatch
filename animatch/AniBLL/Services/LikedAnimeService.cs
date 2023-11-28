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
        void Insert(LikedAnimeModel liked);
        void Delete(LikedAnimeModel liked);
        int GetLastUserId();
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
            List<AniDAL.DataBaseClasses.Anime> likedAnimesFromRepository = 
                _likedAnimeRepository.GetLikedAnimesForUser(userId);

            List<AnimeModel> likedAnimes = likedAnimesFromRepository
                .Select(anime => new AnimeModel
                {
                    Id = anime.Id,
                    Name = anime.Name,
                    Text = anime.Text,
                    Imdbrate = anime.Imdbrate,
                    Photo = anime.Photo,
                    Year = anime.Year
                })
                .ToList();

            return likedAnimes;
        }
        public int GetLastUserId()
        {
            return _likedAnimeRepository.GetLastUserId();
        }
        public void Insert(LikedAnimeModel liked)
        {
            _likedAnimeRepository.Insert(liked);
        }
        public void Delete(LikedAnimeModel liked)
        {
            _likedAnimeRepository.Delete(liked);
        }
    }
}
