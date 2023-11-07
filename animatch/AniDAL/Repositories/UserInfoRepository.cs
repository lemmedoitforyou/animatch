using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;
using Microsoft.EntityFrameworkCore;

namespace AniDAL.Repositories
{
    public interface IUserInfoRepository
    {
        UserInfo GetById(int id);
        UserInfo GetByUsername(string username);
        List<UserInfo> GetAll();
        void Add(int id, string username, string email, string password, string name, int level, string text, string photo, int watchedCount);
        void Update(UserInfo userInfo);
        void Delete(UserInfo userInfo);
        bool IsExistUsername(string username);
        bool IsExistEmail(string email);
        int GetLastUserId();
    }
    public class UserInfoRepository : IUserInfoRepository
    {
        private readonly ApplicationDbContext _context;

        public UserInfoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserInfo GetById(int id)
        {
            return _context.UserInfo.FirstOrDefault(u => u.Id == id);
        }

        public UserInfo GetByUsername(string username)
        {
            return _context.UserInfo.FirstOrDefault(u => u.Username == username);
        }

        public List<UserInfo> GetAll()
        {
            return _context.UserInfo.ToList();
        }

        public void Add(int id, string username, string email, string password, string name, int level, string text, string photo, int watchedCount)
        {
            UserInfo user = new UserInfo
            {
                Id = id,
                Username = username,
                Email = email,
                Password = password,
                Name = "додати ім'я",
                Level = 0,
                Text = "додати підпис",
                Photo = "defaultphoto.jpg",
                WatchedCount = 0
            };
                _context.UserInfo.Add(user);
                _context.SaveChanges();
          
        }

        public void Update(UserInfo userInfo)
        {
            _context.UserInfo.Update(userInfo);
            _context.SaveChanges();
        }

        public void Delete(UserInfo userInfo)
        {
            _context.UserInfo.Remove(userInfo);
            _context.SaveChanges();
        }

        public bool IsExistUsername(string username)
        {
            return _context.UserInfo.Any(u => u.Username == username);
        }

        public bool IsExistEmail(string email)
        {
            return _context.UserInfo.Any(u => u.Email == email);
        }

        public int GetLastUserId()
        {
            int lastUserId = _context.UserInfo.Max(u => u.Id);
            return lastUserId;
        }
    }
}
