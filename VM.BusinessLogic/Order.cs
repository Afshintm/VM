
namespace VM.BusinessLogic
{
    public class Order
    {
        public string OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal PaidBalance { get; set; }
        public string PaymentType { get; set; }
        public string PanNumber { get; set; }
    }
}
