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
    public interface IUserInfoRepository: IGenericRepository<UserInfo>
    {
        UserInfo GetByUsername(string username);
        bool IsExistUsername(string username);
        bool IsExistEmail(string email);
        int GetLastUserId();
        public void UpdateTitleAndText(int userId, string newTitle, string newText);
        public void WatchAnime(int userId);
    }
    public class UserInfoRepository: GenericRepository<UserInfo>, IUserInfoRepository
    {
        public UserInfo GetByUsername(string username)
        {
            return _context.UserInfo.FirstOrDefault(u => u.Username == username);
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
        public void UpdateTitleAndText(int userId, string newTitle, string newText)
        {
            var userInfo = _context.UserInfo.FirstOrDefault(u => u.Id == userId);

            if (userInfo != null)
            {
                userInfo.Name = newTitle;
                userInfo.Text = newText;

                _context.SaveChanges();
            }
        }
        public void WatchAnime(int userId)
        {
            var userInfo = _context.UserInfo.FirstOrDefault(u => u.Id == userId);

            if (userInfo != null)
            {
                userInfo.WatchedCount += 1;

                _context.SaveChanges();
            }
        }
    }
}
