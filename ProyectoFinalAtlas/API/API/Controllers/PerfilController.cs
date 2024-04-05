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
    public class PerfilController : Controller, IPerfilController
    {

        public IPerfilBW _PerfilBW;

        public PerfilController(IPerfilBW perfilBW)
        {
            _PerfilBW = perfilBW;
        }

        [HttpGet]
        [Route("GetPerfilPorIdUser/{IDUsuario}")]
        public async Task<IActionResult> ObtenerUsuarioId(Guid IDUsuario)
        {
            return Ok(await _PerfilBW.GerPerfilPorIdUser(IDUsuario));
        }

        [HttpPost]
        [Route("CrearPerfil/{IDusuario}")]
        public async Task<IActionResult> PostAsync([FromBody] Perfil perfil, Guid IDusuario)
        {
            return Ok(await _PerfilBW.CrearPerfil(perfil,IDusuario));
        }

        [HttpPut]
        [Route("ActualizarPerfil/{IDusuario}")]
        public async Task<IActionResult> ActualizarUsuarioPorId(Guid IDusuario, [FromBody] Perfil perfil)
        {
            return Ok(await _PerfilBW.ActualizarPerfilPorUsuarioId(IDusuario, perfil));
        }
    }
}
