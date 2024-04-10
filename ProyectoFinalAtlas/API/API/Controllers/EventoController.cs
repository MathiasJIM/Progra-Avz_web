using Abstracciones.BW;
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
    }
}
