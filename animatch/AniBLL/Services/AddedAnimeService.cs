using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class AddedAnimeService
    {
        private readonly IAddedAnimeRepository _addedAnimeRepository; 

        public AddedAnimeService(IAddedAnimeRepository addedAnimeRepository)
        {
            _addedAnimeRepository = addedAnimeRepository;
        }

        public List<AddedAnime> GetAnimeAddedByUser(int userId)
        {
            return _addedAnimeRepository.GetAddedAnimesForUser(userId);
        }
    }
}
