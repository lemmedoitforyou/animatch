using AniBLL.Models;
using AniDAL.DataBaseClasses;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IUserService
    {
        UserInfoModel GetById(int id);

        UserInfoModel GetByUsername(string username);

        List<UserInfoModel> GetAll();

        void Insert(UserInfoModel userInfo);

        void Update(UserInfoModel userInfo);

        void Delete(int userInfo);

        bool IsExistUsername(string username);

        bool IsExistEmail(string email);

        int GetLastId();

        void UpdateTitleAndText(int userId, string newTitle, string newText);

        void WatchAnime(int userId);
    }

    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _userRepository;

        public UserService(IUserInfoRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public UserInfoModel GetById(int id)
        {
            var user = this._userRepository.GetById(id);

            if (user == null)
            {
                return null;
            }

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
                WatchedCount = user.WatchedCount,
            };

            return userInfoModel;
        }

        public UserInfoModel GetByUsername(string username)
        {
            var user = this._userRepository.GetByUsername(username);

            if (user == null)
            {
                return null;
            }

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
                WatchedCount = user.WatchedCount,
            };

            return userInfoModel;
        }

        public List<UserInfoModel> GetAll()
        {
            List<UserInfo> userInfos = this._userRepository.GetAll().ToList();

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
                WatchedCount = user.WatchedCount,
            }).ToList();

            return userInfoModels;
        }

        public void Insert(UserInfoModel userInfo)
        {
            this._userRepository.Insert(userInfo);
        }

        public void Update(UserInfoModel userInfo)
        {
            this._userRepository.Update(userInfo);
        }

        public void Delete(int userInfo)
        {
            this._userRepository.Delete(userInfo);
        }

        public bool IsExistUsername(string username)
        {
            return this._userRepository.IsExistUsername(username);
        }

        public bool IsExistEmail(string email)
        {
            return this._userRepository.IsExistEmail(email);
        }

        public int GetLastId()
        {
            return this._userRepository.GetLastUserId();
        }

        public void UpdateTitleAndText(int userId, string newTitle, string newText)
        {
            this._userRepository.UpdateTitleAndText(userId, newTitle, newText);
        }

        public void WatchAnime(int userId)
        {
            this._userRepository.WatchAnime(userId);
        }
    }
}
