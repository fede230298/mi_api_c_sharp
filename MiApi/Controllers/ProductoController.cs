using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Controllers.DTOS;
using MiApi.Repository;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProducto")]
        public List<Producto> GetProductos(int IdUsuario)
        {
            return ProductoHandler.GetProductos(IdUsuario);
        }

        [HttpPost]
        public bool CreateProducto(PostProducto producto)
        {
            return ProductoHandler.CreateProducto(producto);
        }

        [HttpPut]
        public bool UpdateProducto(int Id, PutProducto producto)
        {
            return ProductoHandler.UpdateProducto(Id, producto);
        }

        [HttpDelete]
        public bool DeleteProducto(int Id)
        {
            return ProductoHandler.DeleteProducto(Id);
        }
    }
}
