using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SkelandStore.Core.Entities.Identity;
using SkelandStore.Core.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Skyland.Services.TokenServices
{
    public class TokenServices : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
        {
            //Payload :
            //1)Private Claims[User-Defiened]:
            var AuthClaims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName,user.DisplayName),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var UserRoles = await userManager.GetRolesAsync(user);//to get Roles of user and add it to Private Claims
            foreach (var Role in UserRoles)
            {
                AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            }

            //Key 
            var Encryptionkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            //Form Token Object
            var Token = new JwtSecurityToken
            (//Register Claims
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:ExpirtionDate"])),//meaning From Now make expiredate 2 dayes
                                                                                                 //Private Claims
                claims: AuthClaims,
                //Header (Key,Algorithm Type)
                signingCredentials: new SigningCredentials(Encryptionkey, SecurityAlgorithms.HmacSha256Signature)
            );

            //Generate The Token
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}
