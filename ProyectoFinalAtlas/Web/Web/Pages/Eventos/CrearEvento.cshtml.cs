using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Helpers;

namespace Web.Pages.Eventos
{
    public class CrearEventoModel : PageModel
    {
        private Configuracion _configuracion;

        [BindProperty]
        public Abstracciones.Models.Evento Evento { get; set; } = new Abstracciones.Models.Evento();

        public string Mensaje { get; set; }

        public CrearEventoModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {


            try
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                var cliente = new HttpClient();

                string endPoint = _configuracion.GetEndpoint("CrearEvento").Replace("{IdUsuario}", userID);
                var respuesta = await cliente.PostAsJsonAsync(endPoint, Evento);
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
