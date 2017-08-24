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

            // WARNING: the COMPILER CAN'T DETECT THIS
            var result = first + second;
            return Ok(result);
        }
    }
}
