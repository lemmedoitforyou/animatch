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
    }
}
