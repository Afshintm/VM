namespace VM.BusinessLogic
{
    public static class ExtensionMethods
    {
        public static VendingMachineViewModel ToViewModel(this VendingMachine item)
        {
            if (item == null) return null;
            var result = new VendingMachineViewModel
            {
                BalancePaid = item.BalancePaid,
                CashAmount = item.CashAmount,
                CreditCardAmount = item.CreditCardAmount
            };
            item.ProductStockList.ForEach(x=>result.ProductList.Add(x.ToViewModel()));
            return result;
        }

        public static ProductViewModel ToViewModel(this Product item)
        {
            if (item == null) return null;
            return new ProductViewModel
            {
                AvailableItems = item.AvailableItems,
                Id = item.Id,
                Price = item.Price,
                Name = item.Name,
                MaxItems = item.MaxItems,
                SoldItems = item.SoldItems
            };
        }
    }
}
