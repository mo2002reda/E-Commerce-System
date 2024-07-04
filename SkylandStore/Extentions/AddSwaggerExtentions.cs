using System.Runtime.CompilerServices;

namespace SkylandStore.Extentions
{
    public static class AddSwaggerExtentions
    {
        public static WebApplication AddSwaggerMiddleWares(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
