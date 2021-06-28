using LoginSystem.Services.Models;

namespace LoginSystem.Services.Services
{
    public interface IUserService
    {
        int AddUser(LoginModel regInfoFromForm);
        void DeleteUser(int cacheKey);
        UserModel UpdateUser(int cacheKey, UserModel newUserInfo);
    }
}