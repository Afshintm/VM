using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VM.BusinessLogic
{
    public static class VmConfig
    {
        public const int MaxItemsInVendingMachine = 10;

        public static class ErrorMessages
        {
            public const string ProductNotFound = "Product Not Found.";
            public const string OutOfStock = "Product is out of stock.";

        }

        public static class PaymentType
        {
            public const string CashPaymentType = "Cash";
            public const string CreditCardPaymentType = "CreditCard";

        }
    }

    public static class IdGenerator
    {
        static IdGenerator(){
            _id = 0;
        }

        public static int GetId()
        {
            return ++_id;
        }

        private static int _id;

    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AvailableItems { get; set; }
        public int MaxItems { get; set; }
        public int SoldItems { get; set; }
        public decimal Price { get; set; }

        public Product(string name, int availableItems,  decimal price)
        {
            Name = name;
            Id = IdGenerator.GetId();
            AvailableItems = availableItems;
            Price = price;
            MaxItems = VmConfig.MaxItemsInVendingMachine;
            SoldItems = 0;
        }

        public int LoadItem(int numberOfItems)
        {
            var rejectedItems = 0;
            if (numberOfItems <= 0) return numberOfItems;

            var toBeLoadedItems = MaxItems - AvailableItems;

            if (toBeLoadedItems <= numberOfItems)
            {
                AvailableItems += toBeLoadedItems;
                rejectedItems = numberOfItems-toBeLoadedItems;
            }
            else
            {
                AvailableItems += numberOfItems;
            }

            return rejectedItems;
        }

        public void Restock()
        {
            SoldItems = 0;
            AvailableItems = MaxItems = VmConfig.MaxItemsInVendingMachine;
        }

        public int Sold()
        {
            if (AvailableItems > 0)
            {
                SoldItems++;
                AvailableItems--;
            }
            else
            {
                return -1;
            }
            return AvailableItems;
        }
    }
}
