using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductoVendido")]
        public List<ProductoVendido> GetProductoVendido()
        {
            return ProductoVendidoHandler.GetProductoVendido();
        }
    }
}
