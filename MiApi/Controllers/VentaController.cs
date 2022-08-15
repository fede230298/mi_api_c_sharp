using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;

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
    }
}
