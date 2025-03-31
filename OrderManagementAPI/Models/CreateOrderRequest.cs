namespace OrderManagementAPI.Models
{
    public class CreateOrderRequest
    {
        public int KullaniciId { get; set; }
        public List<OrderItemRequest> Urunler { get; set; } = new();
    }
}
