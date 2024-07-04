using Microsoft.AspNetCore.Identity;
using SkelandStore.Core.Entities.Identity;

namespace SkelandStore.Core.Services
{
    public interface ITokenService
    {
        //Segniture of Function Create Token =>Take User , Return Token {string}
        public Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager);
    }
}
