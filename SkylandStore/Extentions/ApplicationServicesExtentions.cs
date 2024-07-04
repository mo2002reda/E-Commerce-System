using Microsoft.AspNetCore.Mvc;
using SkelandStore.Core.Interface_sRepository;
using SkelandStore.Core.Services;
using Skyland.Services;
using SkyLand.Repository;
using SkyLand.Repository.Data;
using SkylandStore.Errors;
using SkylandStore.Profiles;
namespace SkylandStore.Extentions
{
    public static class ApplicationServicesExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {



            #region Registeration Services
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped(typeof(IOrderServices), typeof(OrderServices));
            //instead of duplicate Code of Registeration ,Use Generic Regestration
            //  Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile())); OR
            Services.AddAutoMapper(typeof(MappingProfile));
            #region Error handling
            Services.Configure<ApiBehaviorOptions>(Options =>
                {//this create an object that appear as avalidation Error
                    Options.InvalidModelStateResponseFactory = (actionContext) =>
                    {//actionContext : is a context of the Current Request
                     //ModelState = Dictionary [Key : value] Key=> Name Of Parameter & Value => error
                     //1)Error Message 
                     //2)Status Code 
                     //3)Trace Id
                        var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                           .SelectMany(p => p.Value.Errors)//fetch all Errors
                                                           .Select(E => E.ErrorMessage)
                                                           .ToArray();

                        var ValidationErrorResponse = new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        };
                        return new BadRequestObjectResult(ValidationErrorResponse);
                    };

                });
            #endregion
            #endregion

            return Services;
        }
    }
}
