using Microsoft.AspNetCore.Mvc;
using MiApi.Model;
using MiApi.Repository;

namespace MiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NombreController
    {
        [HttpGet(Name = "GetName")]
        public string GetName()
            {
                return NombreHandler.GetName();
            }
    }
}
