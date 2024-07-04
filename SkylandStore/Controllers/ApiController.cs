using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SkylandStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        //this controller has Common EndPoint that will exist in all Controller & dataAnotation Of Controller which come from ControllerBase

    }
}
