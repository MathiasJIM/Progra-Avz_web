using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web.Helpers;
using Web.Pages.Noticia;

namespace Web.Pages.Eventos
{
    public class MisEventosModel : PageModel
    {
        private Configuracion _configuracion;

        public MisEventosModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<Evento> Evento { get; set; } = new List<Evento>();

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerEventoPorUsuario").Replace("IDUsuario", userId);
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Evento = JsonConvert.DeserializeObject<List<Evento>>(contenido);
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            string endpoint = _configuracion.GetEndpoint("EliminarEventosPorID").Replace("{IDEvento}", id.ToString());
            var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error al eliminar el evento .");
                return Page();
            }
        }
    }
}
