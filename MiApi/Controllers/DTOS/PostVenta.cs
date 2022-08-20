namespace MiApi.Controllers.DTOS
{
    public class PostVenta
    {
        public int IdProducto { get; set; }

        public int Stock { get; set; }

        public int IdUsuario { get; set; }

        public string Status { get; set; }
    }
}
