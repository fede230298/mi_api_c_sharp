using MiApi.Controllers.DTOS;
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

        public static List<Producto> GetProductos(int IdUsuario)
        {
            List<Producto> resultado = new List<Producto>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryProducto = @"SELECT * FROM Producto WHERE IdUsuario = @IdUsuario";

                SqlParameter parametroIdUsuario = new SqlParameter();
                parametroIdUsuario.ParameterName = "IdUsuario";
                parametroIdUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroIdUsuario.Value = IdUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryProducto, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parametroIdUsuario);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(dataReader["Id"]);
                                producto.Descripciones = dataReader["Descripciones"].ToString();
                                producto.PrecioVenta = Convert.ToInt32(dataReader["PrecioVenta"]);
                                producto.Costo = Convert.ToInt32(dataReader["Costo"]);
                                producto.Stock = Convert.ToInt32(dataReader["Stock"]);
                                producto.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

                                resultado.Add(producto);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultado;
        }
        public static bool CreateProducto(PostProducto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Producto( " +
                    "[Descripciones], " +
                    "[Costo], " +
                    "[PrecioVenta], " +
                    "[Stock], " +
                    "[IdUsuario]) " +
                    "VALUES " +
                    "(@Descripciones, " +
                    "@Costo, " +
                    "@PrecioVenta, " +
                    "@Stock, " +
                    "@IdUsuario)";

                SqlParameter parametroDescripciones = new SqlParameter();
                parametroDescripciones.ParameterName = "Descripciones";
                parametroDescripciones.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroDescripciones.Value = producto.Descripciones;

                SqlParameter parametroCosto = new SqlParameter();
                parametroCosto.ParameterName = "Costo";
                parametroCosto.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroCosto.Value = producto.Costo;

                SqlParameter parametroPrecioVenta = new SqlParameter();
                parametroPrecioVenta.ParameterName = "PrecioVenta";
                parametroPrecioVenta.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroPrecioVenta.Value = producto.PrecioVenta;

                SqlParameter parametroStock = new SqlParameter();
                parametroStock.ParameterName = "Stock";
                parametroStock.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroStock.Value = producto.Stock;

                SqlParameter parametroIdUsuario = new SqlParameter();
                parametroIdUsuario.ParameterName = "IdUsuario";
                parametroIdUsuario.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open();

                {
                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametroDescripciones);
                        sqlCommand.Parameters.Add(parametroCosto);
                        sqlCommand.Parameters.Add(parametroPrecioVenta);
                        sqlCommand.Parameters.Add(parametroStock);
                        sqlCommand.Parameters.Add(parametroIdUsuario);
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            resultado = true;
                        }
                    }

                    sqlConnection.Close();
                }

            }
            return resultado;
        }
        public static bool UpdateProducto(int Id, PutProducto producto)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUser = "UPDATE Producto " +
                "SET Descripciones = @Descripciones, " +
                "Costo = @Costo, " +
                "PrecioVenta = @PrecioVenta, " +
                "Stock = @Stock, " +
                "IdUsuario = @IdUsuario " +
                "WHERE Id = @Id";

                SqlParameter parametroId = new SqlParameter();
                parametroId.ParameterName = "Id";
                parametroId.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroId.Value = Id;

                SqlParameter parametroDescripciones = new SqlParameter();
                parametroDescripciones.ParameterName = "Descripciones";
                parametroDescripciones.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroDescripciones.Value = producto.Descripciones;

                SqlParameter parametroCosto = new SqlParameter();
                parametroCosto.ParameterName = "Costo";
                parametroCosto.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroCosto.Value = producto.Costo;

                SqlParameter parametroPrecioVenta = new SqlParameter();
                parametroPrecioVenta.ParameterName = "PrecioVenta";
                parametroPrecioVenta.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroPrecioVenta.Value = producto.PrecioVenta;

                SqlParameter parametroStock = new SqlParameter();
                parametroStock.ParameterName = "Stock";
                parametroStock.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroStock.Value = producto.Stock;

                SqlParameter parametroIdUsuario = new SqlParameter();
                parametroIdUsuario.ParameterName = "IdUsuario";
                parametroIdUsuario.SqlDbType = System.Data.SqlDbType.BigInt;
                parametroIdUsuario.Value = producto.IdUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUser, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parametroDescripciones);
                    sqlCommand.Parameters.Add(parametroCosto);
                    sqlCommand.Parameters.Add(parametroPrecioVenta);
                    sqlCommand.Parameters.Add(parametroStock);
                    sqlCommand.Parameters.Add(parametroIdUsuario);
                    sqlCommand.Parameters.Add(parametroId);

                    int numberOfRows = sqlCommand.ExecuteNonQuery();
                    if (numberOfRows > 0)
                    {
                        resultado = true;
                    }
                    sqlConnection.Close();
                }

                return resultado;
            }
        }
        public static bool DeleteProducto(int Id)
        {
            bool resultado = false;
            bool deleteProductoVendido = false;
            bool deleteProducto = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM ProductoVendido " +
                    "WHERE IdProducto = @IdProducto";

                SqlParameter sqlParameter = new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = Id;

                sqlConnection.Open();

                {

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            deleteProductoVendido = true;
                        }
                    }

                    sqlConnection.Close();
                }

            }
            if (deleteProductoVendido)
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    string queryDelete = "DELETE FROM Producto " +
                        "WHERE Id = @Id";

                    SqlParameter sqlParameter = new SqlParameter("Id", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = Id;

                    sqlConnection.Open();

                    {

                        using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(sqlParameter);
                            int numberOfRows = sqlCommand.ExecuteNonQuery();
                            if (numberOfRows > 0)
                            {
                                deleteProducto = true;
                            }
                        }

                        sqlConnection.Close();
                    }

                }

            }
            if (deleteProducto)
            {
                resultado = true;
            }
            return resultado;
        }
    }
}
