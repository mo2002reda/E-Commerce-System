namespace SkylandStore.Errors
{
    public class ApiExceptionResponse : ApiResponse
    {
        public string? Details { get; set; }//make it nullable cause in Production state not need details
        public ApiExceptionResponse(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }

    }

}
