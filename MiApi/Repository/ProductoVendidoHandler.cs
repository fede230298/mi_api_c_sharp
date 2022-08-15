using MiApi.Model;
using System.Data.SqlClient;

namespace MiApi.Repository
{
    public class ProductoVendidoHandler
    {
        public const string ConnectionString =
            "Server=LAPTOP-TEA6MRLS;" +
            "Database=SistemaGestion;" +
            "Trusted_Connection=True;";

        public static List<ProductoVendido> GetProductoVendido()
        {
            List<ProductoVendido> resultado = new List<ProductoVendido>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM ProductoVendido", sqlConnection))
                {
                    sqlCommand.Connection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas que leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                ProductoVendido productoVendido = new ProductoVendido();
                                productoVendido._id = Convert.ToInt32(dataReader["Id"]);
                                productoVendido._idProducto = Convert.ToInt32(dataReader["IdProducto"]);
                                productoVendido._stock = Convert.ToInt32(dataReader["Stock"]);
                                productoVendido._idVenta = Convert.ToInt32(dataReader["IdVenta"]);

                                resultado.Add(productoVendido);
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
