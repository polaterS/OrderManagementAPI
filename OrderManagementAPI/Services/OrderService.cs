using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();
        private readonly List<Product> _products = new();
        private int _orderIdCounter = 1;
        private int _orderItemIdCounter = 1;

        public OrderService()
        {
            _products.Add(new Product { Id = 1, Ad = "Laptop", Stok = 10 });
            _products.Add(new Product { Id = 2, Ad = "Telefon", Stok = 5 });
        }

        public (bool Basarili, string Mesaj, Order? Siparis) CreateOrder(CreateOrderRequest request)
        {
            foreach (var item in request.Urunler)
            {
                var urun = _products.FirstOrDefault(p => p.Id == item.UrunId);
                if (urun == null || urun.Stok < item.Adet)
                {
                    return (false, $"Ürün ID {item.UrunId} stokta yok veya bulunamadı.", null);
                }
            }

            foreach (var item in request.Urunler)
            {
                var urun = _products.First(p => p.Id == item.UrunId);
                urun.Stok -= item.Adet;
            }

            var siparis = new Order
            {
                Id = _orderIdCounter++,
                KullaniciId = request.KullaniciId,
                Urunler = request.Urunler.Select(i => new OrderItem
                {
                    Id = _orderItemIdCounter++,
                    UrunId = i.UrunId,
                    Adet = i.Adet
                }).ToList()
            };

            _orders.Add(siparis);
            return (true, "Sipariş başarıyla oluşturuldu.", siparis);
        }

        public List<Order> GetOrdersByUserId(int kullaniciId)
        {
            return _orders.Where(o => o.KullaniciId == kullaniciId).ToList();
        }

        public Order? GetOrderById(int siparisId)
        {
            return _orders.FirstOrDefault(o => o.Id == siparisId);
        }

        public bool DeleteOrder(int siparisId)
        {
            var siparis = _orders.FirstOrDefault(o => o.Id == siparisId);
            if (siparis == null) return false;
            _orders.Remove(siparis);
            return true;
        }
    }
}
