﻿namespace MiApi.Controllers.DTOS
{
    public class PutProducto
    {
        public string Descripciones { get; set; }
        public int Costo { get; set; }
        public int PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int IdUsuario { get; set; }
    }
}
