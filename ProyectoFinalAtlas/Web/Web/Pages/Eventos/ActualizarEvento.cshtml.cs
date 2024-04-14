using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Eventos
{
    public class ActualizarEventoModel : PageModel
    {
        private Configuracion _configuracion;


        public ActualizarEventoModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Models.Evento Evento { get; set; }

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid IDEvento)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerEventoPorID").Replace("{IDEvento}", IDEvento.ToString());
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Evento = JsonConvert.DeserializeObject<Abstracciones.Models.Evento>(contenido);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                string endPoint = _configuracion.GetEndpoint("ActualizarEventoPorID").Replace("{IDEvento}", Evento.ID.ToString());
                var cliente = new HttpClient();
                var respuesta = await cliente.PutAsJsonAsync(endPoint, Evento);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./Calendario");
            }
            catch (HttpRequestException ex)
            {
                var mensajeError = ex.Message;
                ModelState.AddModelError(string.Empty, $"Error al procesar la solicitud: {mensajeError}");
                return Page();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocurrió un error al procesar la solicitud.");
                return Page();
            }
        }
    }
}
