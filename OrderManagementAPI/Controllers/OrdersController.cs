using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementAPI.Models;
using OrderManagementAPI.Services;

namespace OrderManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult SiparisOlustur(CreateOrderRequest request)
        {
            var sonuc = _orderService.CreateOrder(request);
            if (!sonuc.Basarili)
                return BadRequest(sonuc.Mesaj);

            return Ok(sonuc.Siparis);
        }

        [HttpGet("kullanici/{kullaniciId}")]
        public IActionResult KullaniciSiparisleri(int kullaniciId)
        {
            var siparisler = _orderService.GetOrdersByUserId(kullaniciId);
            return Ok(siparisler);
        }

        [HttpGet("{siparisId}")]
        public IActionResult SiparisDetay(int siparisId)
        {
            var siparis = _orderService.GetOrderById(siparisId);
            if (siparis == null)
                return NotFound("Sipariş bulunamadı.");

            return Ok(siparis);
        }

        [HttpDelete("{siparisId}")]
        public IActionResult SiparisSil(int siparisId)
        {
            var basarili = _orderService.DeleteOrder(siparisId);
            if (!basarili)
                return NotFound("Sipariş bulunamadı.");

            return NoContent();
        }
    }
}
