using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return UsuarioHandler.GetUsuarios();
        }

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public List<Usuario> GetLogIn(string nombreUsuario, string contraseña)
        {
            return UsuarioHandler.GetLogIn(nombreUsuario, contraseña);
        }
    }
}
