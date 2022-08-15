namespace MiApi.Model

{
    public class Usuario
    {
        public int _id { get; set; }
        public string _nombre { get; set; }
        public string _apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }
        public string _mail { get; set; }

        /*public Usuario(int id, string nombre, string apellido, string nombreUsuario, string contraseña, string mail)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _contraseña = contraseña;
            _mail = mail;
        }*/
    }
}
