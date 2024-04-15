using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Recurso
{
    public class ActualizarRecursoModel : PageModel
    {
        private Configuracion _configuracion;


        public ActualizarRecursoModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Modelos.Recurso Recurso { get; set; }

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid IDRecurso)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerRecursoPorID").Replace("{IDRecurso}", IDRecurso.ToString());
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Recurso = JsonConvert.DeserializeObject<Abstracciones.Modelos.Recurso>(contenido);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            try
            {
                string endPoint = _configuracion.GetEndpoint("ActualizarRecursoID").Replace("{IDRecurso}", Recurso.ID.ToString());
                var cliente = new HttpClient();
                var respuesta = await cliente.PutAsJsonAsync(endPoint, Recurso);
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
