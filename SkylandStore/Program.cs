using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkelandStore.Core.Entities.Identity;
using SkelandStore.Core.Services;
using SkyLand.Repository;
using SkyLand.Repository.Data;
using SkyLand.Repository.Identity;
using SkylandStore.Extentions;
using SkylandStore.middleWares;
using StackExchange.Redis;
namespace SkylandStore
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configer Services - Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            #region Allow Dependancy Injection For DataBase
            builder.Services.AddDbContext<StoreDbContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnection"));
                    //Configration =>file Exist in program file have settings of appSettings file 
                    //GetConnectionString => function to get The connection

                });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("SecurityConnection"));
            });
            #endregion

            #region Allow Dependancy Injection For Class thet Implement Interface IConnectionMultiplexer (Redis)
            //Using AddSingleton< > cause we want to Connect with Redis per all Requests not every Request open the Connection
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {//add Connection String Of Redis 
                var Connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);//this Open the Connection With the Connection String
            });
            #endregion
            #endregion
            builder.Services.AddApplicationServices();


            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();

            #region Configration - Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //this middle ware used to send execption page details about the exception to Developer : (at .net 6 this middle were included to application by default but at .net 5 Dev must add it to can use it)
                app.UseMiddleware<ExceptionMiddleWare>();//To make clr Exceute the ExceptionMiddleWare class and search about invoke function to call it
                app.AddSwaggerMiddleWares();
            }
            #endregion

            #region Update-database Automatically
            //used Using to Close Scop Automatically /can use Scop.Dispose() also
            using var Scop = app.Services.CreateScope();//Carry all services which have Scopped lifeTime (DBContext,..)
            var Services = Scop.ServiceProvider;//catch all services which in Scop Variable

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();//Create an Object From class that implement ILoggerFactory Interface To can Use It
            try
            {//may be appear Expection when opening database so use Logger Execption to return Execption Message

                var DBContext = Services.GetRequiredService<StoreDbContext>();//Ask CLR For Creating Object From DbContext Explicitly
                await DBContext.Database.MigrateAsync();//Update-DataBase
                var Identity = Services.GetRequiredService<AppIdentityDbContext>();//Create Object From Identity Database
                await Identity.Database.MigrateAsync();//Update-Database
                var User = Services.GetRequiredService<UserManager<AppUser>>();
                await ApplyDbContextSeed.SeedUserAsync(User);
                await StoreSeeding.SeedDataAsync(DBContext);

            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();//create an object Of Logger in Program File(cause the Error will occear when Running {when openning database})
                Logger.LogError(ex, "An Error Occeared when Openning database");
            }
            #endregion

            #region MiddleWare
            app.UseStatusCodePagesWithReExecute("/errors/{0}"); // middleware used if there aren't endpoint Or notfound exception appear this middleware will redirect to ErrorControler
            app.UseHttpsRedirection();//used to Redirect the response to appear
            app.UseStaticFiles();//To Load Images
            app.UseAuthentication();
            app.UseAuthorization();

            #endregion
            app.MapControllers();
            app.Run();
        }
    }
}
