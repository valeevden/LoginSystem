using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Services.Services
{
    public class UserService
    {
        public int AddUser(UserModel userDto)
        {

            userDto.Password = new SecurityService().GetHash(userDto.Password);
            var result = _userRepository.AddUser(userDto);
            if (userDto.Roles != null && userDto.Roles.Count > 0)
            {
                foreach (var role in userDto.Roles)
                {
                    _userRepository.AddRoleToUser(result, (int)role);
                }
            }
            return result;
        }
    }
}
