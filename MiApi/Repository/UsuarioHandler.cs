using MiApi.Controllers.DTOS;
using MiApi.Model;
using System.Data.SqlClient;

namespace MiApi.Repository
{
    public class UsuarioHandler
    {
        public const string ConnectionString =
            "Server=LAPTOP-TEA6MRLS;" +
            "Database=SistemaGestion;" +
            "Trusted_Connection=True;";

        public static bool CreateUsuario(PostUsuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryInsert = "INSERT INTO Usuario( " +
                    "[Nombre], " +
                    "[Apellido], " +
                    "[NombreUsuario], " +
                    "[Contraseña], " +
                    "[Mail]) " +
                    "VALUES " +
                    "(@Nombre, " +
                    "@Apellido, " +
                    "@NombreUsuario, " +
                    "@Contraseña, " +
                    "@Mail)";

                SqlParameter parametroNombre = new SqlParameter();
                parametroNombre.ParameterName = "Nombre";
                parametroNombre.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombre.Value = usuario.Nombre;

                SqlParameter parametroApellido = new SqlParameter();
                parametroApellido.ParameterName = "Apellido";
                parametroApellido.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroApellido.Value = usuario.Apellido;

                SqlParameter parametroNombreUsuario = new SqlParameter();
                parametroNombreUsuario.ParameterName = "NombreUsuario";
                parametroNombreUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombreUsuario.Value = usuario.NombreUsuario;

                SqlParameter parametroContraseña = new SqlParameter();
                parametroContraseña.ParameterName = "Contraseña";
                parametroContraseña.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroContraseña.Value = usuario.Contraseña;

                SqlParameter parametroMail = new SqlParameter();
                parametroMail.ParameterName = "Mail";
                parametroMail.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroMail.Value = usuario.Mail;

                sqlConnection.Open();

                {
                    using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(parametroNombre);
                        sqlCommand.Parameters.Add(parametroApellido);
                        sqlCommand.Parameters.Add(parametroNombreUsuario);
                        sqlCommand.Parameters.Add(parametroContraseña);
                        sqlCommand.Parameters.Add(parametroMail);
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
        public static bool UpdateUsuario(string NombreUsuario, PutUsuario usuario)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUser = "UPDATE Usuario " +
                "SET Nombre = @Nombre, " +
                "Apellido = @Apellido " +
                "WHERE NombreUsuario = @NombreUsuario";

                SqlParameter parametroNombreUsuario = new SqlParameter();
                parametroNombreUsuario.ParameterName = "NombreUsuario";
                parametroNombreUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombreUsuario.Value = NombreUsuario;

                SqlParameter parametroNombre = new SqlParameter();
                parametroNombre.ParameterName = "Nombre";
                parametroNombre.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombre.Value = usuario.Nombre;

                SqlParameter parametroApellido = new SqlParameter();
                parametroApellido.ParameterName = "Apellido";
                parametroApellido.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroApellido.Value = usuario.Apellido;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUser, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parametroNombre);
                    sqlCommand.Parameters.Add(parametroApellido);
                    sqlCommand.Parameters.Add(parametroNombreUsuario);

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
        public static string GetLogIn(string NombreUsuario, string Contraseña)
        {
            string resultado;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUsers = @"SELECT * FROM Usuario " +
                    "WHERE NombreUsuario = @NombreUsuario " +
                    "AND Contraseña = @Contraseña";

                SqlParameter parametroNombreUsuario = new SqlParameter();
                parametroNombreUsuario.ParameterName = "NombreUsuario";
                parametroNombreUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombreUsuario.Value = NombreUsuario;

                SqlParameter parametroContraseña = new SqlParameter();
                parametroContraseña.ParameterName = "Contraseña";
                parametroContraseña.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroContraseña.Value = Contraseña;



                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUsers, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parametroNombreUsuario);
                    sqlCommand.Parameters.Add(parametroContraseña);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            resultado = "Inicio de sesión correcto";
                        }
                        else
                        {
                            resultado = "Error, usuario o contraseña incorrectos";
                        }
                    }
                    sqlConnection.Close();
                }
            }

            return resultado;
        }
        public static bool DeleteUsuario(int id)
        {
            bool resultado = false;

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryDelete = "DELETE FROM Usuario " +
                    "WHERE Id = @id";

                SqlParameter sqlParameter = new SqlParameter("id", System.Data.SqlDbType.BigInt);
                sqlParameter.Value = id;

                sqlConnection.Open();

                {

                    using (SqlCommand sqlCommand = new SqlCommand(queryDelete, sqlConnection))
                    {
                        sqlCommand.Parameters.Add(sqlParameter);
                        int numberOfRows = sqlCommand.ExecuteNonQuery();
                        if (numberOfRows > 0)
                        {
                            resultado = true;
                        }
                    }

                    sqlConnection.Close();
                }

                return resultado;
            }
        }
        public static List<Usuario> GetUsuarios(string NombreUsuario)
        {
            List<Usuario> resultado = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                string queryUser = @"SELECT * FROM Usuario WHERE NombreUsuario = @NombreUsuario";

                SqlParameter parametroNombreUsuario = new SqlParameter();
                parametroNombreUsuario.ParameterName = "NombreUsuario";
                parametroNombreUsuario.SqlDbType = System.Data.SqlDbType.VarChar;
                parametroNombreUsuario.Value = NombreUsuario;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUser, sqlConnection))
                {
                    sqlCommand.Parameters.Add(parametroNombreUsuario);

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario.Nombre = dataReader["Nombre"].ToString();
                                usuario.Apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario.Mail = dataReader["Mail"].ToString();

                                resultado.Add(usuario);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            return resultado;
        }
    }
}
