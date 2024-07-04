using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkelandStore.Core.Entities.Identity;
using System.Security.Claims;

namespace SkylandStore.Extentions
{
    public static class UserManagerExtention
    {
        public async static Task<AppUser> FindUserWithAddressAsync(this UserManager<AppUser> UserManager, ClaimsPrincipal User)
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await UserManager.Users.Include(A => A.Address).FirstOrDefaultAsync(E => E.Email == Email);
            return user;
        }

    }
}
