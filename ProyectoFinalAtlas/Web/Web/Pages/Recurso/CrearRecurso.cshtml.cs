using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Helpers;

namespace Web.Pages.Recurso
{
    public class CrearRecursoModel : PageModel
    {
        private Configuracion _configuracion;

        [BindProperty]
        public Abstracciones.Modelos.Recurso Recurso { get; set; } = new Abstracciones.Modelos.Recurso();

        public string Mensaje { get; set; }

        public CrearRecursoModel(Configuracion configuracion)
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

                string endPoint = _configuracion.GetEndpoint("CrearRecurso").Replace("{IdUsuario}", userID);
                var respuesta = await cliente.PostAsJsonAsync(endPoint, Recurso);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("./Recursos");
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
