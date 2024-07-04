using SkylandStore.Errors;
using System.Text.Json;

namespace SkylandStore.middleWares
{
    public class ExceptionMiddleWare
    {
        private readonly RequestDelegate _next;//will hold the next middleware
        private readonly ILogger<ExceptionMiddleWare> _logger;//will log all errors in ExceptionMiddleWare class
        private readonly IHostEnvironment _host;//carry my current environment

        public ExceptionMiddleWare(RequestDelegate Next, ILogger<ExceptionMiddleWare> logger, IHostEnvironment host)
        {
            _next = Next;
            _logger = logger;
            _host = host;
        }
        //InvokeAsync Function : 
        //1)catch the request(context) and if the request not has any exception it pass this request to the next middleware(next)
        public async Task InvokeAsync(HttpContext context)
        {   //context => my request
            try
            {//if there no exception it will pass it to next middleware
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //if the app in :
                //1)Production State : will log the error in database To seen by support team
                #region Content of Exception Message
                //1)Content Type =>{application/json,text/html,....}
                context.Response.ContentType = "application/json";
                //2)Status Code
                context.Response.StatusCode = 500;//as we are in Internal Server Error
                #endregion
                //2)Development State :Will log the error in Console App
                //if (_host.IsDevelopment())
                //{
                //    var Response = new ApiExceptionResponse(500,ex.Message, ex.StackTrace.ToString());
                //    //the error response message returen as stackTrace(url & numbers & string ) not string only  
                //}
                //else//this mean we are in Production Environment
                //{
                //    var Response = new ApiExceptionResponse(500);
                //}
                var Response = _host.IsDevelopment() ? new ApiExceptionResponse(500, ex.Message, ex.StackTrace.ToString()) : new ApiExceptionResponse(500);
                var Options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,//To make the Response Name as camelCase cause java script that used in front understand only camelCase Naming
                };

                var JsonResponse = JsonSerializer.Serialize(Response, Options);//cause Response is an Object so must Convert it to json to can appear 
                context.Response.WriteAsync(JsonResponse);
            }

        }
    }
}
