using System;

namespace VM.BusinessLogic
{
    public class ChangeItem : IComparable<ChangeItem>, IComparable
    {
        public ChangeItem(int changeValue)
        {
            ChangeValue = changeValue;
        }
        public int ChangeValue { get; set; }
        public int Count { get; set; }
        public int CompareTo(ChangeItem other)
        {
            if (other == null) return 1;
            if (ChangeValue < other.ChangeValue) return -1;
            if (ChangeValue == other.ChangeValue) return 0;
            return 1;
        }
        public int CompareTo(object obj)
        {
            return CompareTo((ChangeItem)obj);
        }

        public override string ToString()
        {
            if (Count > 0)
                return $"{Count} " + GetCoinType() + " coin" + (Count > 1 ? "s" : string.Empty);

            return string.Empty;
        }

        public string GetCoinType()
        {
            var result = string.Empty;
            if (ChangeValue / 100 > 0)
                result = $"${ChangeValue / 100}";
            else
            {
                result = $"{ChangeValue} ¢";
            }
            return result;
        }
    }
}
