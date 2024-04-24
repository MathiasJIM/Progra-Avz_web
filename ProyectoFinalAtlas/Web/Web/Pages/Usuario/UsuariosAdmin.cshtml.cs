using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;
using Web.Helpers;
using System.Net.Http.Headers;

namespace Web.Pages.Usuario
{
    public class UsuariosAdminModel : PageModel
    {
        private Configuracion _configuracion;

        public UsuariosAdminModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<Abstracciones.Modelos.UsuariosExistentes> Usuarios { get; set; } = new List<Abstracciones.Modelos.UsuariosExistentes>();
        public string Rol { get; set; }
        public string Mensaje { get; set; }

        public List<UsuariosExistentes> Administradores { get; set; } = new List<UsuariosExistentes>();
        public List<UsuariosExistentes> Profesores { get; set; } = new List<UsuariosExistentes>();
        public List<UsuariosExistentes> Estudiantes { get; set; } = new List<UsuariosExistentes>();


        public async Task<IActionResult> OnGetLoadAsync(string NombreRol)
        {
            Rol = NombreRol;
            string endPoint = _configuracion.GetEndpoint("MostrarUsuarios") + $"?NombreRol={Uri.EscapeDataString(NombreRol)}";
            using (var cliente = new HttpClient())
            {
                var respuesta = await cliente.GetAsync(endPoint);
                if (!respuesta.IsSuccessStatusCode)
                {
                    Mensaje = "Error al cargar recursos: " + respuesta.ReasonPhrase;
                    return Page();
                }

                var contenido = await respuesta.Content.ReadAsStringAsync();
                var allUsers = JsonConvert.DeserializeObject<List<UsuariosExistentes>>(contenido);

                // Directly filter users based on roleName if necessary
                Usuarios = allUsers.Where(u => u.NombreRol.Equals(NombreRol, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            string endpoint = _configuracion.GetEndpoint("EliminarUsuarioPorID").Replace("{UsuarioID}", id.ToString());
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
                ModelState.AddModelError(string.Empty, "Error al eliminar usuario.");
                return Page();
            }
        }



    }
}
