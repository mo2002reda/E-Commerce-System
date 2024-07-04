using Microsoft.AspNetCore.Identity;
using SkelandStore.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace SkyLand.Repository.Identity
{
    public static class ApplyDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())//Check If There isn't any Users In User Table 
            {
                var User = new AppUser
                {
                    DisplayName = "Mohamed Reda",
                    Email = "Mr2438844@gmail.com",
                    PhoneNumber = "011294521452",
                    UserName = "Mr2438844",
                };
                await userManager.CreateAsync(User, "P@$$w0rd");
            }
        }
    }
}
