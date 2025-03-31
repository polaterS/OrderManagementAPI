using OrderManagementAPI.Models;

namespace OrderManagementAPI.Services
{
    public interface IOrderService
    {
        (bool Basarili, string Mesaj, Order? Siparis) CreateOrder(CreateOrderRequest request);
        List<Order> GetOrdersByUserId(int kullaniciId);
        Order? GetOrderById(int siparisId);
        bool DeleteOrder(int siparisId);
    }
}
