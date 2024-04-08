using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;
using NoticiaModel = Abstracciones.Modelos.Noticia;

namespace Web.Pages.Noticia
{
    public class NoticiasModel : PageModel
    {
        private Configuracion _configuracion;

        public NoticiasModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<NoticiaModel> Noticias { get; set; } = new List<NoticiaModel>();

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("MostrarNoticias");
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Noticias = JsonConvert.DeserializeObject<List<NoticiaModel>>(contenido);
            return Page();
        }
    }
}
