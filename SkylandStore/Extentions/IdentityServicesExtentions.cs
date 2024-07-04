using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SkelandStore.Core.Entities.Identity;
using SkelandStore.Core.Services;
using Skyland.Services.TokenServices;
using SkyLand.Repository.Identity;
using System.Security.Claims;
using System.Text;

namespace SkylandStore.Extentions
{
    public static class IdentityServicesExtentions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddScoped<ITokenService, TokenServices>();
            Services.AddIdentity<AppUser, IdentityRole>()//add the Identity Package's Interfaces
                                .AddEntityFrameworkStores<AppIdentityDbContext>();//Add classes which implement Identity Interfaces
            //Allow Dependanct Injection For => {User Mager , SignIn Manager,Role manager} 
            Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;//this is AuthentationSchema which will Combared with Sent Schema
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; //this is an ChallengeScheme which sent with Token
            })
                    .AddJwtBearer(Options =>//this Options Will Validate at the Token Parameters
                    {
                        Options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            #region Options Which Form Token Which we Will Validate On it
                            /*
                              1)issuer
                              2)audience
                              3)expires       
                              4)EncryptionKey
                            */
                            #endregion
                            ValidateIssuer = true,//=> Validation on Issuer which exist in app Sitting
                            ValidIssuer = _configuration["Jwt:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = _configuration["Jwt:Audience"],
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                        };
                    });

            return Services;


        }
    }
}
