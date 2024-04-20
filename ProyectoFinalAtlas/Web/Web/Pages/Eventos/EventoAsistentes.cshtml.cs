using Abstracciones.Modelos;
using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web.Helpers;

namespace Web.Pages.Eventos
{
    public class EventoAsistentesModel : PageModel
    {
        private Configuracion _configuracion;

        public EventoAsistentesModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<UsuarioAsistente> Asistentes { get; set; } = new List<UsuarioAsistente>();

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid IDEvento)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerAsistentesPorIdEvento").Replace("{IDEvento}", IDEvento.ToString());
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Asistentes = JsonConvert.DeserializeObject<List<UsuarioAsistente>>(contenido);
            return Page();
        }

    }
}
