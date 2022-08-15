using MiApi.Model;
using System.Data.SqlClient;

namespace MiApi.Repository
{
    public class ProductoHandler
    {
        public const string ConnectionString =
            "Server=LAPTOP-TEA6MRLS;" +
            "Database=SistemaGestion;" +
            "Trusted_Connection=True;";

        public static List<Producto> GetProductos()
        {
            List<Producto> resultado = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Producto", sqlConnection))
                {
                    sqlCommand.Connection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas que leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto._id = Convert.ToInt32(dataReader["Id"]);
                                producto._stock = Convert.ToInt32(dataReader["Stock"]);
                                producto._idUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
                                producto._costo = Convert.ToInt32(dataReader["Costo"]);
                                producto._precioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto._descripciones = dataReader["Descripciones"].ToString();

                                resultado.Add(producto);
                            }
                        }
                    }
                    sqlCommand.Connection.Close();
                }
            }
            return resultado;
        }
    }
}
