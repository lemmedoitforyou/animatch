using System.Collections.Generic;
using System.Linq;
using AniDAL.Repositories;
using AniBLL.Models;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public interface IUserService
    {
        UserInfoModel GetById(int id);
        UserInfoModel GetByUsername(string username);
        List<UserInfoModel> GetAll();
        void Insert(UserInfoModel userInfo);
        void Update(UserInfoModel userInfo);
        void Delete(UserInfoModel userInfo);
        bool IsExistUsername(string username);
        bool IsExistEmail(string email);
        int GetLastUserId();
        void UpdateTitleAndText(int userId, string newTitle, string newText);
    }

    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _userRepository;

        public UserService(IUserInfoRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfoModel GetById(int id)
        {
            var user = _userRepository.GetById(id);

            // Перевірка на null, якщо користувача не знайдено
            if (user == null)
            {
                return null; // або можна кинути виняток чи повернути якусь іншу логіку за замовчуванням
            }

            // Створення екземпляра UserInfoModel на основі об'єкта з репозиторію
            var userInfoModel = new UserInfoModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                Level = user.Level,
                Text = user.Text,
                Photo = user.Photo,
                WatchedCount = user.WatchedCount
            };

            return userInfoModel;
        }


        public UserInfoModel GetByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);

            // Перевірка на null, якщо користувача не знайдено
            if (user == null)
            {
                return null; // або можна кинути виняток чи повернути якусь іншу логіку за замовчуванням
            }

            // Створення екземпляра UserInfoModel на основі об'єкта з репозиторію
            var userInfoModel = new UserInfoModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                Level = user.Level,
                Text = user.Text,
                Photo = user.Photo,
                WatchedCount = user.WatchedCount
            };

            return userInfoModel;
        }


        public List<UserInfoModel> GetAll()
        {
            List <UserInfo> userInfos  = _userRepository.GetAll().ToList();

            List<UserInfoModel> userInfoModels = userInfos.Select(user => new UserInfoModel
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Name = user.Name,
                Level = user.Level,
                Text = user.Text,
                Photo = user.Photo,
                WatchedCount = user.WatchedCount
            }).ToList();

            return userInfoModels;
        }


        public void Insert(UserInfoModel userInfo)
        {
            _userRepository.Insert(userInfo);
        }

        public void Update(UserInfoModel userInfo)
        {
            _userRepository.Update(userInfo);
        }

        public void Delete(UserInfoModel userInfo)
        {
            _userRepository.Delete(userInfo);
        }

        public bool IsExistUsername(string username)
        {
            return _userRepository.IsExistUsername(username);
        }

        public bool IsExistEmail(string email)
        {
            return _userRepository.IsExistEmail(email);
        }

        public int GetLastUserId()
        {
            return _userRepository.GetLastUserId();
        }

        public void UpdateTitleAndText(int userId, string newTitle, string newText)
        {
            _userRepository.UpdateTitleAndText(userId, newTitle, newText);
        }
    }
}
