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
    public interface IDislikedAnimeService
    {
        List<AnimeModel> GetDislikedAnimesForUser(int userId);
        void Insert(DislikedAnimeModel disliked);
        void Delete(DislikedAnimeModel disliked);
    }
    public class DislikedAnimeService : IDislikedAnimeService
    {
        private readonly IDislikedAnimeRepository _dislikedAnimeRepository; 

        public DislikedAnimeService(IDislikedAnimeRepository dislikedAnimeRepository)
        {
            _dislikedAnimeRepository = dislikedAnimeRepository;
        }

        public List<AnimeModel> GetDislikedAnimesForUser(int userId)
        {
            List<AniDAL.DataBaseClasses.Anime> dislikedAnimeRepository =
                _dislikedAnimeRepository.GetDislikedAnimesForUser(userId);

            List<AnimeModel> dislikedAnime = dislikedAnimeRepository
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

            return dislikedAnime;
        }
        public void Insert(DislikedAnimeModel disliked)
        {
            _dislikedAnimeRepository.Insert(disliked);
        }
        public void Delete(DislikedAnimeModel disliked)
        {
            _dislikedAnimeRepository.Delete(disliked);
        }
    }
}
