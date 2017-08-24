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
#if !DEBUG
             // JsonConvert.DeserializeObject("test");
#endif
            return Ok(first + second);
        }
    }
}
