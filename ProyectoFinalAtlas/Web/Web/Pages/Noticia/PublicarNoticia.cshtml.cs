using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Helpers;

namespace Web.Pages.Noticia
{
    [Authorize(Roles = "1,2")]
    public class PublicarNoticiaModel : PageModel
    {
        private Configuracion _configuracion;

        [BindProperty]
        public Abstracciones.Modelos.Noticia Noticia { get; set; } = new Abstracciones.Modelos.Noticia();

        public IFormFile FotoNoticia { get; set; }

        public PublicarNoticiaModel(Configuracion configuracion)
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
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "Noticias", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await FotoNoticia.CopyToAsync(stream);
                    }

                    Noticia.imagen = $"/images/Noticias/{fileName}";
                }

                string endPoint = _configuracion.GetEndpoint("PublicarNoticia").Replace("{IdUsuario}", userID);
                var respuesta = await cliente.PostAsJsonAsync(endPoint, Noticia);
                respuesta.EnsureSuccessStatusCode();

                return RedirectToPage("Noticias");
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
