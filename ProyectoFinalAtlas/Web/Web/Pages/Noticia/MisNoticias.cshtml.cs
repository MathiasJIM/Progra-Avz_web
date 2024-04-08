using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Web.Helpers;
using NoticiaModel = Abstracciones.Modelos.Noticia;

namespace Web.Pages.Noticia
{
    public class MisNoticiasModel : PageModel
    {
        private Configuracion _configuracion;

        public MisNoticiasModel(Configuracion configuracion)
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

            string endPoint = _configuracion.GetEndpoint("ObtenerNoticiasPorIdUsuario").Replace("IDUsuario", userId);
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Noticias = JsonConvert.DeserializeObject<List<NoticiaModel>>(contenido);
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            string endpoint = _configuracion.GetEndpoint("EliminarNoticiaPorID").Replace("{IDNoticia}", id.ToString());
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
                ModelState.AddModelError(string.Empty, "Error al eliminar la noticia.");
                return Page();
            }
        }

    }


}
