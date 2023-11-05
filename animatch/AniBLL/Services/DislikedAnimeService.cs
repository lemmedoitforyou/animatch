using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class DislikedAnimeService
    {
        private readonly IDislikedAnimeRepository _dislikedAnimeRepository; 

        public DislikedAnimeService(IDislikedAnimeRepository dislikedAnimeRepository)
        {
            _dislikedAnimeRepository = dislikedAnimeRepository;
        }

        public List<DislikedAnime> GetDislikedAnimeByUser(int userId)
        {
            return _dislikedAnimeRepository.GetDislikedAnimesForUser(userId);
        }
    }
}
