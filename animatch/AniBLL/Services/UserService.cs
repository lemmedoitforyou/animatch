using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;

namespace AniBLL.Services
{
    public class UserService
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
