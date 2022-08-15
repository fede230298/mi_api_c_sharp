using MiApi.Model;
using System.Data.SqlClient;

namespace MiApi.Repository
{
    public class VentaHandler
    {
        public const string ConnectionString =
            "Server=LAPTOP-TEA6MRLS;" +
            "Database=SistemaGestion;" +
            "Trusted_Connection=True;";

        public static List<Venta> GetVenta()
        {
            List<Venta> resultado = new List<Venta>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Venta", sqlConnection))
                {
                    sqlCommand.Connection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        // Me aseguro que haya filas que leer
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();
                                venta._id = Convert.ToInt32(dataReader["Id"]);
                                venta._comentarios = dataReader["Comentarios"].ToString();

                                resultado.Add(venta);
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
