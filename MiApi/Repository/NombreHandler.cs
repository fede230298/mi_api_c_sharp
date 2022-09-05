using MiApi.Model;

namespace MiApi.Repository
{
    public class NombreHandler
    {
        public static string GetName()
        {
            Nombre nombre = new Nombre();
            return nombre.NombreTienda;
        }
    }
}
