using System.Web.Http;
using System.Web.Http.Cors;
using VM.BusinessLogic;

namespace VM.Controllers
{
    [RoutePrefix("api/v1/admin")]
    [EnableCors("*", "*", "*")]
    public class AdminController : ApiController
    {
        private IVendingMachine _vendingMachine;
        public AdminController(IVendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }
        [HttpGet]
        [Route("status")]
        public IHttpActionResult GetStatus()
        {
            
                dynamic result = new System.Dynamic.ExpandoObject();
                result.CashAmount = _vendingMachine.CashAmount;
                result.CreditCardAmount = _vendingMachine.CreditCardAmount;
                return Ok(result);
            
        }
        [HttpPost]
        [Route("restock")]
        public IHttpActionResult Restock()
        {
            _vendingMachine.Restock();
            return Ok("Vending Machine is restocked successfully.");

        }

    }
}
