using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkylandStore.Errors;

namespace SkylandStore.Controllers
{
    [Route("errors/{Code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]//to ignore this controller at swagger cuse it can't do the documnetation for this controller as it required detect all verbs of end Points
    public class ErrorController : ControllerBase
    {
        public ActionResult Error(int Code)//Kestral will Consuming the verb 
        {
            return NotFound(new ApiExceptionResponse(Code));
        }
    }

}
