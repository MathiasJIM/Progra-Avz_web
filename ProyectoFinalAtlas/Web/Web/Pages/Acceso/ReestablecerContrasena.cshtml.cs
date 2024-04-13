using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Acceso
{
    public class ReestablecerContrasenaModel : PageModel
    {

        private Configuracion _configuracion;

        [BindProperty]
        public Abstracciones.Modelos.CambioContrasena Request { get; set; }

        public ReestablecerContrasenaModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var Hash = Password.GenerarHash(Request.NuevaContraseñaHash);
                Request.NuevaContraseñaHash = Password.ObtenerHash(Hash);
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
                var userid = userIdClaim.Value;
                Request.UsuarioId = Guid.Parse(userid);
                string endPoint = _configuracion.GetEndpoint("CambiarContrasena");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsJsonAsync<Abstracciones.Modelos.CambioContrasena>(endPoint, Request);
                respuesta.EnsureSuccessStatusCode();
                var resultado = JsonConvert.DeserializeObject<Guid>(await respuesta.Content.ReadAsStringAsync());

                return RedirectToPage("../Index");
            }
            catch (HttpRequestException ex)
            {
                // Captura la excepción de la solicitud HTTP y obtén el mensaje de error
                var mensajeError = ex.Message;
                ModelState.AddModelError(string.Empty, $"Error al procesar la solicitud: {mensajeError}");
                return Page();
            }
            catch (Exception ex)
            {
                // Captura cualquier otra excepción y muestra un mensaje genérico
                ModelState.AddModelError(string.Empty, "Ocurrió un error al procesar la solicitud.");
                return Page();
            }
        }
    }
}
