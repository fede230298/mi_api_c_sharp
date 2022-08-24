using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;
using MiApi.Controllers.DTOS;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVenta")]
        public List<Venta> GetVenta()
        {
            return VentaHandler.GetVenta();
        }

        [HttpPost]
        public List<PostVenta> CreateVenta(List<PostVenta> DetalleVenta)
        {
            return VentaHandler.CreateVenta(DetalleVenta);
        }
        [HttpDelete]
        public bool DeleteVenta(int idVenta)
        {
            return VentaHandler.DeleteVenta(idVenta);
        }
    }
}
