using System.Web.Http;

namespace Calculator.API.Controllers
{
    [RoutePrefix("api/calculator")]
    public class CalculatorController : ApiController
    {
        [HttpGet]
        [Route("add/{first}/{second}")]
        public IHttpActionResult Add([FromUri] int first, [FromUri] int second)
        {
            var result = first + second + 10000;
            return Ok(result);
        }
    }
}
