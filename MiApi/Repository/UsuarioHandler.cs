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

        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> resultado = new List<Usuario>();

            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Usuario", sqlConnection))
                {
                    sqlConnection.Open();

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario._id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario._nombre = dataReader["Nombre"].ToString();
                                usuario._apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario._mail = dataReader["Mail"].ToString();

                                resultado.Add(usuario); 
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }

            return resultado;
        }

        public static List<Usuario> GetLogIn(string NombreUsuario, string Contraseña)
        {
            List<Usuario> resultado = new List<Usuario>();

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
                            while (dataReader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario._id = Convert.ToInt32(dataReader["Id"]);
                                usuario.NombreUsuario = dataReader["NombreUsuario"].ToString();
                                usuario._nombre = dataReader["Nombre"].ToString();
                                usuario._apellido = dataReader["Apellido"].ToString();
                                usuario.Contraseña = dataReader["Contraseña"].ToString();
                                usuario._mail = dataReader["Mail"].ToString();

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
