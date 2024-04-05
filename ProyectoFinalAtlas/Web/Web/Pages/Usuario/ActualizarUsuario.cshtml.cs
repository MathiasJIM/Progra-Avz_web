using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.Helpers;
using Microsoft.Extensions.Configuration;
using Abstracciones.Modelos;
using System.Net.Http.Json;
using System.Reflection;
using System.Text;

namespace Web.Pages.Usuario
{
    public class ActualizarUsuarioModel : PageModel
    {
        private Configuracion _configuracion;


        public ActualizarUsuarioModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Modelos.Usuario Usuario { get; set; }


        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerUsuarioPorId").Replace("{id}", userId);
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Usuario = JsonConvert.DeserializeObject<Abstracciones.Modelos.Usuario>(contenido);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            // Crear un objeto Usuario con solo los campos que se van a actualizar
            var usuarioParaActualizar = new Abstracciones.Modelos.ActualizarUsuario
            {
                NombreCompleto = Usuario.NombreCompleto,
                CorreoElectronico = Usuario.CorreoElectronico
            };

            string endPointActualizar = _configuracion.GetEndpoint("ActualizarUsuarioId").Replace("{id}", userId);
            var cliente = new HttpClient();
            var respuestaActualizar = await cliente.PutAsJsonAsync(endPointActualizar, usuarioParaActualizar);
            respuestaActualizar.EnsureSuccessStatusCode();

            Mensaje = "Usuario actualizado correctamente.";
            return RedirectToPage("/Index");
        }













    }
}
