namespace CoffeeShopApi.Model
{
    public class OrderModel
    {
        public int OrderID { get; set; }

        public string OrderNumber { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerID { get; set; }

        public string  PaymentMode { get; set; }

        public string TotalAmount { get; set; }

        public string ShippingAddress { get; set; }

        public int UserID { get; set; }
    }
}
