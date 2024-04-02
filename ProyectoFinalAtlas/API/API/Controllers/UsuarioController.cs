using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Abstracciones.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller, IUsuarioController
    {
        public IUsuarioBW _UsuarioBW;

        public UsuarioController(IUsuarioBW usuarioBW)
        {
            _UsuarioBW = usuarioBW;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("AgregarUsuario")]
        public async Task<IActionResult> PostAsync([FromBody] RegistroUsuario usuario)
        {
            return Ok(await _UsuarioBW.RegistrarUsuario(usuario));
        }
    }
}
