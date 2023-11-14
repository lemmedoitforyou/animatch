using System;
using System.Collections.Generic;
using AniDAL.Repositories;
using AniDAL.DataBaseClasses;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using System.Xml.Linq;


namespace AniBLL.Services
{
    public interface IUserService
    {
        UserInfo GetById(int id);
        UserInfo GetByUsername(string username);
        List<UserInfo> GetAll();
        void Insert(UserInfo userInfo);
        void Update(UserInfo userInfo);
        void Delete(UserInfo userInfo);
        bool IsExistUsername(string username);
        bool IsExistEmail(string email);
        int GetLastUserId();
    }

    public class UserService : IUserService
    {
        private readonly IUserInfoRepository _userRepository;

        public UserService(IUserInfoRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserInfo GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public UserInfo GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public List<UserInfo> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }

        public void Insert(UserInfo userInfo)
        {
            _userRepository.Insert(userInfo);
        }

        public void Update(UserInfo userInfo)
        {
            _userRepository.Update(userInfo);
        }

        public void Delete(UserInfo userInfo)
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
    }
}