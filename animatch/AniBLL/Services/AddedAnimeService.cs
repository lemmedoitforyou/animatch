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
    public interface IAddedAnimeService
    {
        List<AnimeModel> GetAddedAnimesForUser(int userId);
        void Add(AddedAnimeModel added);
        void Delete(AddedAnimeModel added);
    }
    public class AddedAnimeService: IAddedAnimeService
    {
        private readonly IAddedAnimeRepository _addedAnimeRepository; 

        public AddedAnimeService(IAddedAnimeRepository addedAnimeRepository)
        {
            _addedAnimeRepository = addedAnimeRepository;
        }

        public List<AnimeModel> GetAddedAnimesForUser(int userId)
        {
            List<AniDAL.DataBaseClasses.Anime> addedAnimeRepository = 
                _addedAnimeRepository.GetAddedAnimesForUser(userId);

            // Перетворення об'єктів Anime на об'єкти AnimeModel
            List<AnimeModel> addedAnime = addedAnimeRepository
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

            return addedAnime;
        }
        public void Add(AddedAnimeModel added)
        {
            _addedAnimeRepository.Insert(added);
        }
        public void Delete(AddedAnimeModel added)
        {
            _addedAnimeRepository.Delete(added);
        }
    }
}
