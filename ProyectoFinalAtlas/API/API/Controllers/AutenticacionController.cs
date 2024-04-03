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

    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller, IAutenticacionController
    {
        public IAutenticacionBW _AutenticacionBW;

        public AutenticacionController(IAutenticacionBW autenticacionBW)
        {
            _AutenticacionBW = autenticacionBW;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Post([FromBody] Login loginRequest)
        {
            return Ok(_AutenticacionBW.Login(loginRequest));
        }

        [HttpGet]
        [Route("Validar")]
        public IActionResult GetValidarAutenticacion()
        {
            return Ok("Token Valido");
        }

        [HttpGet("ObtenerUsuarioPorCorreo")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorCorreo(string correoElectronico)
        {
            var usuario = await _AutenticacionBW.ObtenerUsuario(new Usuario() { CorreoElectronico = correoElectronico });
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

    }
}
