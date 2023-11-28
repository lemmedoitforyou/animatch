using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IDislikedAnimeService
    {
        List<Anime> GetDislikedAnimesForUser(int userId);
        void Insert(DislikedAnime disliked);
        void Delete(DislikedAnime disliked);
    }
    public class DislikedAnimeService : IDislikedAnimeService
    {
        private readonly IDislikedAnimeRepository _dislikedAnimeRepository; 

        public DislikedAnimeService(IDislikedAnimeRepository dislikedAnimeRepository)
        {
            _dislikedAnimeRepository = dislikedAnimeRepository;
        }

        public List<Anime> GetDislikedAnimesForUser(int userId)
        {
            return _dislikedAnimeRepository.GetDislikedAnimesForUser(userId);
        }
        public void Insert(DislikedAnime disliked)
        {
            _dislikedAnimeRepository.Insert(disliked);
        }
        public void Delete(DislikedAnime disliked)
        {
            _dislikedAnimeRepository.Delete(disliked);
        }
    }
}
