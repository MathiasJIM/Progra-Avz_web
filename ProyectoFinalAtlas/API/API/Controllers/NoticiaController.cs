using Abstracciones.BW;
using Abstracciones.Models;
using BW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticiaController : Controller
    {
        public INoticiaBW _NoticiaBW;

        public NoticiaController(INoticiaBW noticiaBW)
        {
            _NoticiaBW = noticiaBW;
        }

        [HttpPost]
        [Route("CrearNoticia/{IdUsuario}")]
        public async Task<IActionResult> PostAsync([FromBody] Abstracciones.Models.Noticia noticia, Guid IdUsuario)
        {
            return Ok(await _NoticiaBW.CrearNoticia(noticia, IdUsuario));
        }

        [HttpGet]
        [Route("ObtenerNoticiasPorUsuario/{IDUsuario}")]
        public async Task<IActionResult> ObtenerNoticiasPorUsuario(Guid IDUsuario)
        {
            return Ok(await _NoticiaBW.ObtenerNoticiasPorUsuario(IDUsuario));
        }


        [HttpGet]
        [Route("ObtenerNoticiaPorID/{IDNoticia}")]
        public async Task<IActionResult> ObtenerNoticiaPorID(Guid IDNoticia)
        {
            return Ok(await _NoticiaBW.ObtenerNoticiaPorID(IDNoticia));
        }

        [HttpGet]
        [Route("MostrarNoticias")]
        public async Task<IActionResult> MostrarNoticias()
        {
            return Ok(await _NoticiaBW.MostrarNoticias());
        }

        [HttpPut]
        [Route("ActualizarNoticiaPorID/{IDNoticia}")]
        public async Task<IActionResult> ActualizarNoticiaPorID(Guid IDNoticia, [FromBody] Noticia noticia)
        {
            return Ok(await _NoticiaBW.ActualizarNoticiaPorID(IDNoticia, noticia));
        }

        [HttpDelete]
        [Route("EliminarNoticiasPorUsuario/{IDusuario}")]
        public async Task<IActionResult> EliminarNoticiasPorUsuario(Guid IDNoticia)
        {
            return Ok(await _NoticiaBW.EliminarNoticiasPorUsuario(IDNoticia));
        }

        [HttpPost]
        [Route("EliminarNoticiaPorID/{IDNoticia}")]
        public async Task<IActionResult> EliminarNoticiaPorID(Guid IDNoticia)
        {
            await _NoticiaBW.EliminarNoticiaPorID(IDNoticia);
            return Ok("Noticia eliminada con exito!");
        }
    }
}
