using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProducto")]
        public List<Producto> GetProductos()
        {
            return ProductoHandler.GetProductos();
        }
    }
}
