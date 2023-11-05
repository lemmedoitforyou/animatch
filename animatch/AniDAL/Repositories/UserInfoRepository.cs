using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AniDAL.DataBaseClasses;
using AniDAL.DbContext;

namespace AniDAL.Repositories
{
    public interface IUserInfoRepository
    {
        UserInfo GetById(int id);
        UserInfo GetByUsername(string username);
        List<UserInfo> GetAll();
        void Add(UserInfo userInfo);
        void Update(UserInfo userInfo);
        void Delete(UserInfo userInfo);
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

        public void Add(UserInfo userInfo)
        {
            _context.UserInfo.Add(userInfo);
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
    }
}
