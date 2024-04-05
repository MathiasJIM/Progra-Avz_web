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

namespace Web.Pages.Perfil
{
    public class ActualizarPerfilModel : PageModel
    {
        private Configuracion _configuracion;


        public ActualizarPerfilModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Modelos.Perfil Perfil { get; set; }

        public IFormFile FotoPerfil { get; set; }

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerPerfilPorIdUsuario").Replace("{IDUsuario}", userId);
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Perfil = JsonConvert.DeserializeObject<Abstracciones.Modelos.Perfil>(contenido);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "No se pudo obtener el ID del usuario.");
                return Page();
            }

            var userId = userIdClaim.Value;

            try
            {
                if (!string.IsNullOrEmpty(Perfil.FotoPerfil))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Perfil.FotoPerfil.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                if (FotoPerfil != null)
                {
                    string fileName;
                    if (FotoPerfil.FileName.EndsWith(".jpg"))
                    {
                        fileName = $"{Perfil.Nombre}_foto.jpg";
                    }
                    else
                    {
                        fileName = $"{Perfil.Nombre}_foto.png";
                    }
                    
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Perfiles", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FotoPerfil.CopyToAsync(stream);
                    }

                    Perfil.FotoPerfil = $"/images/Perfiles/{fileName}";
                }

                // Actualizar el perfil en el servidor
                string endPoint = _configuracion.GetEndpoint("ActualizarPerfil").Replace("{IDusuario}", userId);
                var cliente = new HttpClient();
                var respuesta = await cliente.PutAsJsonAsync(endPoint, Perfil);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("../Index");
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
