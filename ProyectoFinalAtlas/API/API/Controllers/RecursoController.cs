using Abstracciones.BW;
using Abstracciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : Controller
    {
        public IRecursoBW _RecursoBW;

        public RecursoController(IRecursoBW recursoBW)
        {
            _RecursoBW = recursoBW;
        }

        [HttpPost]
        [Route("CrearRecurso/{IdUsuario}")]
        public async Task<IActionResult> PostAsync([FromBody] Abstracciones.Models.Recurso recurso, Guid IdUsuario)
        {
            await _RecursoBW.CrearRecurso(recurso, IdUsuario);
            return Ok("Usuario creado con exito!");
        }


        [HttpGet]
        [Route("MostrarRecursos")]
        public async Task<IActionResult> MostrarNoticias()
        {
            return Ok(await _RecursoBW.MostrarRecursos());
        }

        [HttpGet]
        [Route("ObtenerRecursoPorID/{IDRecurso}")]
        public async Task<IActionResult> ObtenerNoticiaPorID(Guid IDRecurso)
        {
            return Ok(await _RecursoBW.ObtenerRecursoPorID(IDRecurso));
        }

        [HttpPut]
        [Route("ActualizarRecursoID/{IDRecurso}")]
        public async Task<IActionResult> ActualizarNoticiaPorID(Guid IDRecurso, [FromBody] Recurso recurso)
        {
            return Ok(await _RecursoBW.ActualizarRecursoID(IDRecurso, recurso));
        }

        [HttpPost]
        [Route("EliminarRecursoPorID/{IDRecurso}")]
        public async Task<IActionResult> EliminarNoticiaPorID(Guid IDRecurso)
        {
            await _RecursoBW.EliminarRecursoPorID(IDRecurso);
            return Ok("Evento eliminado con exito!");
        }

        [HttpGet]
        [Route("ObtenereRecursosPorUsuario/{IDUsuario}")]
        public async Task<IActionResult> ObtenerNoticiasPorUsuario(Guid IDUsuario)
        {
            return Ok(await _RecursoBW.ObtenereRecursosPorUsuario(IDUsuario));
        }
    }
}
