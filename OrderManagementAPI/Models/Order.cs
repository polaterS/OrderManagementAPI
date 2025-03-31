namespace OrderManagementAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int KullaniciId { get; set; }
        public List<OrderItem> Urunler { get; set; } = new();
    }
}
