using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Helpers;

namespace Web.Pages.Acceso
{
    public class RegistroModel : PageModel
    {
        private Configuracion _configuracion;

        [BindProperty]
        public RegistroUsuario Usuario { get; set; }

        public RegistroModel(Configuracion configuracion)
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
                var Hash = Password.GenerarHash(Usuario.ContrasenaHash);
                var HashC = Password.GenerarHash(Usuario.ConfirmarContrasena);
                Usuario.ContrasenaHash = Password.ObtenerHash(Hash);
                Usuario.ConfirmarContrasena = Password.ObtenerHash(HashC);

                string endPoint = _configuracion.GetEndpoint("AgregarUsuario");
                var cliente = new HttpClient();
                var respuesta = await cliente.PostAsJsonAsync<RegistroUsuario>(endPoint, Usuario);
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
