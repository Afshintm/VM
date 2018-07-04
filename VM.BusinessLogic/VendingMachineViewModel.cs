using System.Collections.Generic;

namespace VM.BusinessLogic
{
    public class VendingMachineViewModel
    {
        public VendingMachineViewModel()
        {
            ProductList = new List<ProductViewModel>();
        }

        public decimal CashAmount { get; set; }
        public decimal CreditCardAmount { get; set; }
        public decimal BalancePaid { get; set; }

        public List<ProductViewModel> ProductList { get; set; }
    }
}