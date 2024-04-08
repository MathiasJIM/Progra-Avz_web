using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Noticia
{
    public class EditarNoticiaModel : PageModel
    {
        private Configuracion _configuracion;


        public EditarNoticiaModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Modelos.Noticia Noticia { get; set; }

        public IFormFile FotoNoticia { get; set; }

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid IDNoticia)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenerNoticiaPorId").Replace("{IDNoticia}", IDNoticia.ToString());
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Noticia = JsonConvert.DeserializeObject<Abstracciones.Modelos.Noticia>(contenido);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                ModelState.AddModelError(string.Empty, "No se pudo obtener el ID del usuario.");
                return Page();
            }

            var userId = userIdClaim.Value;

            try
            {
                if (!string.IsNullOrEmpty(Noticia.imagen))
                {
                    var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", Noticia.imagen.TrimStart('/'));
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
                if (FotoNoticia != null)
                {
                    string fileName;
                    if (FotoNoticia.FileName.EndsWith(".jpg"))
                    {
                        fileName = $"{Noticia.Titulo}_foto.jpg";
                    }
                    else
                    {
                        fileName = $"{Noticia.Titulo}_foto.png";
                    }

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Perfiles", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FotoNoticia.CopyToAsync(stream);
                    }

                    Noticia.imagen = $"/images/Perfiles/{fileName}";
                }
                // Actualizar el perfil en el servidor
                string endPoint = _configuracion.GetEndpoint("ActualizarNoticiaPorID").Replace("{IDNoticia}", Noticia.ID.ToString());
                var cliente = new HttpClient();
                var respuesta = await cliente.PutAsJsonAsync(endPoint, Noticia);
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
