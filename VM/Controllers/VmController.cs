using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using VM.BusinessLogic;

namespace VM.Controllers
{
    [RoutePrefix("api/v1")]
    [EnableCors("*", "*", "*")]
    public class VmController : ApiController
    {
        private IVendingMachine _vendingMachine;
        public VmController(IVendingMachine vendingMachine)
        {
            _vendingMachine = vendingMachine;
        }
        [HttpGet]
        [Route("products")]
        public async Task<List<ProductViewModel>> GetProductList()
        {
            var result = Task.Run(() => {
                return _vendingMachine.GetInStockAvaialableProducts().Select(x=>x.ToViewModel()).ToList();
            });
            return await result;
        }

        [Route("order")]
        [HttpPost]
        public OrderResultViewModel Order(Order t)
        {
            try
            {
                var change = _vendingMachine.Purchased(t.ProductId, t.PaidBalance);
                var result = new OrderResultViewModel{ProductId = t.ProductId,ChangeValue = change.Value, OrderId = t.OrderId};
                if (change.Value >= 0.05m) result.ChangeSetup = change.ToString();
                return result;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
        }


        [HttpGet]
        [Route("change/{amount:decimal}")]
        public IHttpActionResult DoSomething(decimal amount)
        {
            Change c;
            try
            {
                if (amount >= 0.05m)
                {
                    c = new Change(amount);
                    return Ok(c.ToString());
                }
                return Ok("No Change.");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
