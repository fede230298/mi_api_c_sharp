namespace MiApi.Model

{
    public class Producto
    {
        public int _id { get; set; }
        public string _descripciones { get; set; }
        public int _costo { get; set; }
        public int _precioVenta { get; set; }
        public int _stock { get; set; }
        public int _idUsuario { get; set; }

        /*public Producto(int id, string descripcion, int costo, int precioVenta, int stock, int idUsuario)
        {
            _id = id;
            _descripciones = descripcion;
            _costo = costo;
            _precioVenta = precioVenta;
            _stock = stock;
            _idUsuario = idUsuario;
        }*/
    }
}