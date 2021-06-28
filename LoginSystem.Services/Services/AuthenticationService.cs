using LoginSystem.Services.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LoginSystem.Services.Services
{
    public class AuthenticationService
    {
        private MemoryCacheSingleton _userCache;

        const string ISSUER = "LoginSystem.Api";
        const string AUDIENCE = "LoginSystem";
        const string KEY = "sdcbsdcbhopwqeihf@#$@@#$45dfolmjioioqhcx";
        const int LIFETIME = 3000;

        public AuthenticationService(MemoryCacheSingleton userCache)
        {
            _userCache = userCache;
        }
        public async Task<LeadDto> GetAuthenticatedLeadAsync(string login)
        {
            return await _repo.GetLeadCredentialsAsync(null, login);
        }
        public AuthenticationResponse GenerateToken(LeadDto lead)
        {
            var identity = GetIdentity(lead);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(LIFETIME)),
                    signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthenticationResponse
            {
                Token = encodedJwt
            };
        }
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }

        private ClaimsIdentity GetIdentity(LeadDto lead)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, lead.Login),
                    new Claim(ClaimTypes.NameIdentifier, lead.Id.ToString()),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, lead.Role.ToString())
                };

            ClaimsIdentity claimsIdentity =
            new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
