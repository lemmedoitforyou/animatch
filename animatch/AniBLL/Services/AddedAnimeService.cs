using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IAddedAnimeService
    {
        List<Anime> GetAddedAnimesForUser(int userId);
        void Add(AddedAnime added);
        void Delete(AddedAnime added);
    }
    public class AddedAnimeService: IAddedAnimeService
    {
        private readonly IAddedAnimeRepository _addedAnimeRepository; 

        public AddedAnimeService(IAddedAnimeRepository addedAnimeRepository)
        {
            _addedAnimeRepository = addedAnimeRepository;
        }

        public List<Anime> GetAddedAnimesForUser(int userId)
        {
            return _addedAnimeRepository.GetAddedAnimesForUser(userId);
        }
        public void Add(AddedAnime added)
        {
            _addedAnimeRepository.Insert(added);
        }
        public void Delete(AddedAnime added)
        {
            _addedAnimeRepository.Delete(added);
        }
    }
}
