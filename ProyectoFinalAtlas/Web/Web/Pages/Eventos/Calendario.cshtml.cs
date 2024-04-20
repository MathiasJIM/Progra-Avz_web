using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
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

        public string UserId { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;
            UserId = userId;

            string endPoint = _configuracion.GetEndpoint("MostrarEventos");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Evento = JsonConvert.DeserializeObject<List<Abstracciones.Models.Evento>>(contenido);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid IDEvento)
        {
            try
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                var usuarioId = Guid.Parse(userID);
                var cliente = new HttpClient();

                string urlVerificar = _configuracion.GetEndpoint("VerificarAsistencia")
                    .Replace("{IDEvento}", IDEvento.ToString())
                    .Replace("{IdUsuario}", usuarioId.ToString());

                var respuestaVerificar = await cliente.GetAsync(urlVerificar);
                if (respuestaVerificar.IsSuccessStatusCode)
                {
                    var content = await respuestaVerificar.Content.ReadAsStringAsync();
                    int resultado = int.Parse(content); 

                    if (resultado == 1)
                    {
                        TempData["ErrorMessage"] = "Usted ya esta registrado en este evento.";
                        return RedirectToPage("./Calendario");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Error al verificar la asistencia. Por favor, intente de nuevo.";
                    return RedirectToPage("./Calendario");
                }

                // Proceder con la inscripción si no está registrado
                string endPoint = _configuracion.GetEndpoint("AnadirAsistencia");
                var datosAsistencia = new
                {
                    IDEvento = IDEvento,
                    IdUsuario = usuarioId
                };

                var respuesta = await cliente.PostAsJsonAsync(endPoint, datosAsistencia);
                respuesta.EnsureSuccessStatusCode();

                TempData["SuccessMessage"] = "Usted ha sido registrado en el evento.";
                return RedirectToPage("./Calendario");
            }
            catch (HttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, $"Error al procesar la solicitud: {ex.Message}");
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
