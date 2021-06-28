using LoginSystem.Services.Models;

namespace LoginSystem.Services.Services
{
    public interface IUserService
    {
        int AddUser(LoginModel regInfoFromForm);
        UserModel GetUserFromCacheById(int id);
        void DeleteUser(int id);
        UserModel UpdateUser(int id, UserModel newUserInfo);
    }
}