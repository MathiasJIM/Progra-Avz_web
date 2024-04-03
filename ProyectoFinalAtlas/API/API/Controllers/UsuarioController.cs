using Abstracciones.API;
using Abstracciones.BW;
using Abstracciones.Modelos;
using Abstracciones.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace API.Controllers
{
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

        [HttpPut]
        [Route("ActualizarUsuario/{id}")]
        public async Task<IActionResult> ActualizarUsuarioPorId(Guid id, [FromBody] Abstracciones.Models.ActualizarUsuario usuario)
        {
            return Ok(await _UsuarioBW.ActualizarUsuarioPorId(id, usuario));
        }



        [HttpGet]
        [Route("ObtenerUsuarioId/{id}")]
        public async Task<IActionResult> ObtenerUsuarioId(Guid id)
        {
            return Ok(await _UsuarioBW.ObtenerUsuarioPorId(id));
        }

        [HttpGet]
        [Route("ObtenerRol/{id}")]
        public async Task<IActionResult> ObtenerRol(int id)
        {
            return Ok(await _UsuarioBW.ObtenerNombreRolPorId(id));
        }

        [HttpGet]
        [Route("ObtenerEstado/{id}")]
        public async Task<IActionResult> ObtenerEstado(int id)
        {
            return Ok(await _UsuarioBW.ObtenerEstadoPorId(id));
        }



    }
}
