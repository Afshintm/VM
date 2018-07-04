using System;
using System.Collections.Generic;

namespace VM.BusinessLogic
{
    public class Change
    {
        public decimal Value { get; }
        public List<ChangeItem> ChangeItems { get; set; }

        public Change(decimal amount)
        {
            Value = amount;
            Init(amount);
        }

        void Init(decimal amount)
        {
            Initialize();
            DivideToCoins(Convert.ToInt32(amount * 100));
        }

        public string GetChange(decimal amount)
        {
            Init(amount);
            return ToString();
        }
        private void Initialize()
        {
            ChangeItems = new List<ChangeItem>
            {
                //5-50 cents coins 
                new ChangeItem(5), 
                new ChangeItem(10),
                new ChangeItem(20),
                new ChangeItem(50),

                // 1 and  2 dollar coins
                new ChangeItem(100),
                new ChangeItem(200)
            };
        }
        
        private void DivideToCoins(int amount)
        {
            var amountLeft = amount;

            ChangeItems.Sort();
            ChangeItems.Reverse();
            foreach (var item in ChangeItems)
            {
                while (amountLeft >= item.ChangeValue)
                {
                    amountLeft -= item.ChangeValue;
                    item.Count++;
                }
            }
        }

        public override string ToString()
        {
            var returnMessage = string.Empty;
            foreach (var item in ChangeItems)
            {
                if (item.Count > 0)
                    returnMessage += item.ToString() + ", ";

            }
            return returnMessage;
        }
    }
}
