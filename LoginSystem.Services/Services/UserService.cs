using LoginSystem.Services.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Services.Services
{
    public class UserService : IUserService
    {
        private MemoryCacheSingleton _userCache;

        public UserService(MemoryCacheSingleton userCache)
        {
            _userCache = userCache;
        }

        //public UserService()
        //{
        //    if (_userCache is null)
        //    {
        //        _userCache = MemoryCacheSingleton.GetCacheInstance();
        //    }
        //}

        public int AddUser(LoginModel regInfoFromForm)
        {
            var newUser = new UserModel();
            var id = (int)DateTime.Now.Ticks;
            newUser.Login = regInfoFromForm.Login;
            newUser.Password = regInfoFromForm.Password;
            newUser.PasswordHash = new SecurityService().GetHash(regInfoFromForm.Password);

            _userCache.Cache.Set<UserModel>(id, newUser);
            return id;
        }

        public UserModel GetUserFromCacheById(int id)
        {
            var userfromCache = _userCache.Cache.Get<UserModel>(id);
            return userfromCache;
        }

        public void DeleteUser(int id)
        {
            _userCache.Cache.Remove(id);
            return;
        }

        public UserModel UpdateUser(int id, UserModel newUserInfo)
        {
            var userToUpdate = _userCache.Cache.Get<UserModel>(id);

            if (userToUpdate.Login != newUserInfo.Login)
            {
                userToUpdate.Login = newUserInfo.Login;
            }
            if (userToUpdate.Email != newUserInfo.Email || newUserInfo.Email != null)
            {
                userToUpdate.Email = newUserInfo.Email;
            }
            if (userToUpdate.Password != newUserInfo.Password)
            {
                userToUpdate.Password = newUserInfo.Password;
                userToUpdate.PasswordHash = new SecurityService().GetHash(newUserInfo.Password);
            }

            _userCache.Cache.Set<UserModel>(id, userToUpdate);
            return userToUpdate;
        }
    }
}
