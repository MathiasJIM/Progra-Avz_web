using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.Helpers;
namespace Web.Pages.Eventos
{
    public class CalendarioModel : PageModel
    {
        private Configuracion _configuracion;

        public CalendarioModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<Abstracciones.Models.Evento> Evento { get; set; } = new List<Abstracciones.Models.Evento>();

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("MostrarEventos");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Evento = JsonConvert.DeserializeObject<List<Abstracciones.Models.Evento>>(contenido);
            return Page();
        }
    }
}
