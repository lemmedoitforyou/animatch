using AniDAL.DataBaseClasses;
using AniDAL.Repositories;

namespace AniBLL.Services
{
    public interface IUserService
    {
        UserInfo GetUserById(int userId);
        UserInfo GetUserByUsername(string username);
    }

    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _userRepository;

        public UserService(IUserInfoRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfo GetUserById(int userId)
        {
            return _userRepository.GetById(userId);
        }

        public UserInfo GetUserByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
    }
}