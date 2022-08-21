﻿using MiApi.Controllers.DTOS;
using MiApi.Model;
using System.Data;
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
                string queryProducto = "SELECT Venta.Id AS 'Venta Nro', " +
                    "Producto.Descripciones AS 'Producto Vendido', " +
                    "ProductoVendido.Stock AS 'Cantidad Vendida', " +
                    "Venta.Comentarios " +
                    "FROM Venta " +
                    "INNER JOIN ProductoVendido " +
                    "ON Venta.Id = ProductoVendido.IdVenta " +
                    "INNER JOIN Producto " +
                    "ON Producto.Id = ProductoVendido.IdProducto";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryProducto, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(dataReader["Venta Nro"]);
                                venta.Comentarios = dataReader["Producto Vendido"].ToString();

                                resultado.Add(venta);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultado;
        }
        public static List<PostVenta> CreateVenta(List<PostVenta> DetalleVenta)
        {
            DataTable tablaProducto = new DataTable();
            DataTable tablaUsuario = new DataTable();
            DataRow[] singlequery;
            DataTable tablaIdVenta = new DataTable();
            string query;
            int registros = 0;
            int stock_producto = 0;
            int cont = -1;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT Id, Stock FROM Producto", sqlConnection);
                sqlConnection.Open();
                SqlAdapter.Fill(tablaProducto);
                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT Id FROM Usuario", sqlConnection);
                sqlConnection.Open();
                SqlAdapter.Fill(tablaUsuario);
                sqlConnection.Close();
            }

            foreach (var VentaProducto in DetalleVenta)
            {
                cont++;
                query = "Id = " + VentaProducto.IdProducto.ToString();
                singlequery = tablaProducto.Select(query);
                stock_producto = Convert.ToInt32(singlequery[0].ItemArray[1]) - VentaProducto.Stock;

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        string QueryUpdate = "INSERT INTO Venta ( [Comentarios], [IdUsuario] ) VALUES ( @Comentarios, @IdUsuario )";


                        SqlParameter parametroComentarios = new SqlParameter();
                        parametroComentarios.ParameterName = "Comentarios";
                        parametroComentarios.SqlDbType = System.Data.SqlDbType.VarChar;
                        parametroComentarios.Value = DateTime.Now;


                        SqlParameter parametroIdUsuario = new SqlParameter();
                        parametroIdUsuario.ParameterName = "IdUsuario";
                        parametroIdUsuario.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametroIdUsuario.Value = VentaProducto.IdUsuario;

                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand(QueryUpdate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(parametroComentarios);
                            sqlCommand.Parameters.Add(parametroIdUsuario);
                            registros = sqlCommand.ExecuteNonQuery();
                        }
                        if (registros > 0)
                        {
                            using (SqlConnection sqlConnection_id = new SqlConnection(ConnectionString))
                            {
                                SqlDataAdapter SqlAdapter = new SqlDataAdapter("SELECT max(Id) FROM Venta", sqlConnection);
                                SqlAdapter.Fill(tablaIdVenta);
                            }                        
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    DetalleVenta[cont].Status = "(Error al ingresar venta: " + ex.Message + ". En INSERT VENTAS";
                }

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        string QueryInsert = "INSERT INTO ProductoVendido ( Stock, IdProducto, IdVenta ) VALUES ( @Stock, @IdProducto, @IdVenta )";

                        SqlParameter parametroStock = new SqlParameter();
                        parametroStock.ParameterName = "Stock";
                        parametroStock.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametroStock.Value = VentaProducto.Stock;

                        SqlParameter parametroIdProducto = new SqlParameter();
                        parametroIdProducto.ParameterName = "IdProducto";
                        parametroIdProducto.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametroIdProducto.Value = VentaProducto.IdProducto;

                        SqlParameter parametroIdVenta = new SqlParameter();
                        parametroIdProducto.ParameterName = "IdVenta";
                        parametroIdProducto.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametroIdProducto.Value = tablaIdVenta.Rows[0].ItemArray[0];

                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand(QueryInsert, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(parametroStock);
                            sqlCommand.Parameters.Add(parametroIdProducto);
                            sqlCommand.Parameters.Add(parametroIdVenta);
                            registros = sqlCommand.ExecuteNonQuery();
                            sqlCommand.Parameters.Clear();
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    DetalleVenta[cont].Status = "Venta No Registrada - Error al ingresar venta: " + ex.Message +". En INSERT PRODUCTO VENDIDO";
                }

                try
                {
                    using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                    {
                        string QueryUpdate = "UPDATE Producto SET Stock = " + stock_producto + " WHERE Id = @IdProducto";

                        SqlParameter parametroIdProducto = new SqlParameter();
                        parametroIdProducto.ParameterName = "IdProducto";
                        parametroIdProducto.SqlDbType = System.Data.SqlDbType.BigInt;
                        parametroIdProducto.Value = VentaProducto.IdProducto;

                        sqlConnection.Open();
                        using (SqlCommand sqlCommand = new SqlCommand(QueryUpdate, sqlConnection))
                        {
                            sqlCommand.Parameters.Add(parametroIdProducto);
                            registros = sqlCommand.ExecuteNonQuery();
                            sqlCommand.Parameters.Clear();
                        }
                        if (registros == 1)
                        {
                            DetalleVenta[cont].Status = "Venta Registrada - Id Venta: " + tablaIdVenta.Rows[0].ItemArray[0] + " - IdUsuario: " + VentaProducto.IdUsuario;
                        }
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    DetalleVenta[cont].Status = "Venta No Registrada - Error al ingresar venta: " + ex.Message + ". En UPDATE Producto";
                }
            }
            return DetalleVenta;
        }
    }
}
