using System.Collections.Generic;

namespace VM.BusinessLogic
{
    public interface IVendingMachine
    {
        List<Product> ProductStockList { get; set; }
        decimal CashAmount { get; set; }
        decimal CreditCardAmount { get; set; }
        void InitializeVendingMachine();
        void Restock();
        Change Purchased(int id , decimal balancePaid, string paymentType = VmConfig.PaymentType.CashPaymentType);
        List<Product> GetInStockAvaialableProducts();
    }
}