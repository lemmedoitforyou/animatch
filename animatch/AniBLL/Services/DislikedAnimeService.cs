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
        List<DislikedAnime> GetDislikedAnimesForUser(int userId);
        void Add(DislikedAnime disliked);
        void Delete(DislikedAnime disliked);
    }
    public class DislikedAnimeService : IDislikedAnimeService
    {
        private readonly IDislikedAnimeRepository _dislikedAnimeRepository; 

        public DislikedAnimeService(IDislikedAnimeRepository dislikedAnimeRepository)
        {
            _dislikedAnimeRepository = dislikedAnimeRepository;
        }

        public List<DislikedAnime> GetDislikedAnimesForUser(int userId)
        {
            return _dislikedAnimeRepository.GetDislikedAnimesForUser(userId);
        }
        public void Add(DislikedAnime disliked)
        {
            _dislikedAnimeRepository.Add(disliked);
        }
        public void Delete(DislikedAnime disliked)
        {
            _dislikedAnimeRepository.Delete(disliked);
        }
    }
}
