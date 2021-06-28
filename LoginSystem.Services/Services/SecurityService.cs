using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Services.Services
{
    class SecurityService : ISecurityService
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string GetHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public bool VerifyPassword(string hashFromCash, string enteredPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashFromCash);
        }
    }
}
