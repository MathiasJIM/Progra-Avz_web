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
    public class CrearPerfilModel : PageModel
    {
        private Configuracion _configuracion;

        [BindProperty]
        public Abstracciones.Modelos.Perfil Perfil { get; set; } = new Abstracciones.Modelos.Perfil();
        
        public IFormFile FotoPerfil { get; set; }

        public CrearPerfilModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var userID = User.Claims.FirstOrDefault(c => c.Type == "Id").Value;
                var cliente = new HttpClient();

                if (FotoPerfil != null)
                {
                    var fileName = $"{Perfil.Nombre}_foto";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Perfiles", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FotoPerfil.CopyToAsync(stream);
                    }

                    Perfil.FotoPerfil = $"/images/Perfiles/{fileName}";
                }

                string endPoint = _configuracion.GetEndpoint("CrearPerfil").Replace("{IDusuario}", userID);
                var respuesta = await cliente.PostAsJsonAsync(endPoint, Perfil);
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
