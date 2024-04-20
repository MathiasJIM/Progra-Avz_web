using Abstracciones.BW;
using Abstracciones.Models;
using BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : Controller
    {
        public IEventoBW _EventoBW;

        public EventoController(IEventoBW eventoBW)
        {
            _EventoBW = eventoBW;
        }

        [HttpPost]
        [Route("CrearEvento/{IdUsuario}")]
        public async Task<IActionResult> PostAsync([FromBody] Abstracciones.Models.Evento evento, Guid IdUsuario)
        {
            await _EventoBW.CrearEvento(evento, IdUsuario);
            return Ok("Usuario creado con exito!");
        }


        [HttpGet]
        [Route("MostrarEventos")]
        public async Task<IActionResult> MostrarNoticias()
        {
            return Ok(await _EventoBW.MostrarEventos());
        }

        [HttpGet]
        [Route("ObtenerEventoPorID/{IDEvento}")]
        public async Task<IActionResult> ObtenerEventoPorID(Guid IDEvento)
        {
            return Ok(await _EventoBW.ObtenerEventoPorID(IDEvento));
        }

        [HttpPut]
        [Route("ActualizarEventoPorID/{IDEvento}")]
        public async Task<IActionResult> ActualizarNoticiaPorID(Guid IDEvento, [FromBody] Evento evento)
        {
            return Ok(await _EventoBW.ActualizarEventoID(IDEvento, evento));
        }

        [HttpPost]
        [Route("EliminarEventoPorID/{IDEvento}")]
        public async Task<IActionResult> EliminarNoticiaPorID(Guid IDEvento)
        {
            await _EventoBW.EliminarEventoPorID(IDEvento);
            return Ok("Evento eliminado con exito!");
        }

        [HttpGet]
        [Route("ObtenerEventoPorUsuario/{IDUsuario}")]
        public async Task<IActionResult> ObtenerNoticiasPorUsuario(Guid IDUsuario)
        {
            return Ok(await _EventoBW.ObtenereEventosPorUsuario(IDUsuario));
        }


        [HttpPost]
        [Route("AnadirAsistencia")]
        public async Task<IActionResult> PostAsync([FromBody] DatosAsistencia datos)
        {
            await _EventoBW.AddAsistencia(datos.IDEvento, datos.IdUsuario);
            return Ok("Asistente Añadido");
        }

        [HttpPost]
        [Route("EliminarAsistencia/{IDEvento}")]
        public async Task<IActionResult> EliminarAsistencia(Guid IDEvento, Guid IdUsuario)
        {
            await _EventoBW.EliminarAsistenciaPorID(IDEvento, IdUsuario);
            return Ok("Asistencia eliminada con exito!");
        }


        [HttpGet]
        [Route("ObtenerAsistentesPorIdEvento/{IDEvento}")]
        public async Task<IActionResult> ObtenerAsistentesPorIdEvento(Guid IDEvento)
        {
            return Ok(await _EventoBW.ObtenerAsistentesPorIdEvento(IDEvento));
        }

        [HttpGet]
        [Route("VerificarAsistencia/{IDEvento}/{IdUsuario}")]
        public async Task<IActionResult> VerificarAsistencia(Guid IDEvento, Guid IdUsuario)
        {
            return Ok(await _EventoBW.VerificarAsistencia(IDEvento, IdUsuario));
        }
    }
}
