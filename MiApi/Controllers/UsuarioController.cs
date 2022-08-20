using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;
using MiApi.Controllers.DTOS;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios(string NombreUsuario)
        {
            return UsuarioHandler.GetUsuarios(NombreUsuario);
        }

        [HttpGet("{nombreUsuario}/{contraseña}")]
        public string GetLogIn(string nombreUsuario, string contraseña)
        {
            return UsuarioHandler.GetLogIn(nombreUsuario, contraseña);
        }

        [HttpPut]
        public bool UpdateUsuario(string NombreUsuario, PutUsuario usuario)
        {
            return UsuarioHandler.UpdateUsuario(NombreUsuario, usuario);
        }

        [HttpDelete]
        public bool DeleteUser([FromBody] int id)
        {
            return UsuarioHandler.DeleteUsuario(id);
        }

        /*[HttpPut]
        public void ModifyUser([FromBody] PutUsuario usuario)
        {

        }*/

        [HttpPost]
        public bool CreateUsuario([FromBody] PostUsuario usuario) 
        {
            return UsuarioHandler.CreateUsuario(usuario);
        }
    }
}
