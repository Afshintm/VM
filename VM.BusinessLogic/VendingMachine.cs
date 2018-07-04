using System.Collections.Generic;
using System.Linq;

namespace VM.BusinessLogic
{
    public class VendingMachine : IVendingMachine
    {
        private List<Product> _productStockList;
        public List<Product> ProductStockList
        {
            get => _productStockList ?? (_productStockList = new List<Product>());
            set => _productStockList = value;
        }
        public decimal CashAmount { get; set; }
        public decimal CreditCardAmount { get; set; }
        public decimal BalancePaid { get; set; }

        public VendingMachine()
        {
            InitializeVendingMachine();
        }

        public void InitializeVendingMachine()
        {
            ProductStockList= new List<Product>
            {
                new Product("Coac",VmConfig.MaxItemsInVendingMachine,2.50m),
                new Product("Fanta", VmConfig.MaxItemsInVendingMachine, 2.75m),
                new Product("Pepsi",VmConfig.MaxItemsInVendingMachine,2.50m),
                new Product("Coac Zero", VmConfig.MaxItemsInVendingMachine, 2.75m),
                new Product("Solo",VmConfig.MaxItemsInVendingMachine,3.20m),
                new Product("Lemonade", VmConfig.MaxItemsInVendingMachine, 2.25m),
                new Product("Orange juce",VmConfig.MaxItemsInVendingMachine,2.70m),
                new Product("Super Drink", VmConfig.MaxItemsInVendingMachine, 4.75m),
                new Product("Red Bull", VmConfig.MaxItemsInVendingMachine, 3.75m),
                new Product("Sport Drink", VmConfig.MaxItemsInVendingMachine, 4.25m)
            };  
        }
        public void Restock()
        {
            ProductStockList.ForEach(x => x.Restock());
            CreditCardAmount = 0.0m;
            CashAmount = 0.0m;

        }

        public Change Purchased(int id , decimal balancePaid, string paymentType = VmConfig.PaymentType.CashPaymentType)
        {
            decimal change = balancePaid;
            var product = ProductStockList.FirstOrDefault(x => x.Id == id && x.AvailableItems>0);
            if (product == null)
                throw new KeyNotFoundException(VmConfig.ErrorMessages.ProductNotFound);
            if (product.Sold() < 0) return new Change(change);

            if(paymentType== VmConfig.PaymentType.CashPaymentType)
                    CashAmount += product.Price;
            else 
                CreditCardAmount+= product.Price;

            change = balancePaid - product.Price;
            if (change < 0.05m)
                CashAmount += change;
            return new Change(change);

        }

        public List<Product> GetInStockAvaialableProducts()
        {
            return ProductStockList.Where(x => x.AvailableItems > 0).ToList();
        }
    }
 }
