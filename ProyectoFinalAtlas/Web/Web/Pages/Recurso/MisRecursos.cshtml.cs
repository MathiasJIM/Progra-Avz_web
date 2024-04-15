using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Web.Helpers;

namespace Web.Pages.Recurso
{
    public class MisRecursosModel : PageModel
    {
        private Configuracion _configuracion;

        public MisRecursosModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<Abstracciones.Modelos.Recurso> Recursos { get; set; } = new List<Abstracciones.Modelos.Recurso>();

        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;

            string endPoint = _configuracion.GetEndpoint("ObtenereRecursosPorUsuario").Replace("IDUsuario", userId);
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            respuesta.EnsureSuccessStatusCode();
            var contenido = await respuesta.Content.ReadAsStringAsync();
            Recursos = JsonConvert.DeserializeObject<List<Abstracciones.Modelos.Recurso>>(contenido);
            return Page();
        }


        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            string endpoint = _configuracion.GetEndpoint("EliminarRecursoPorID").Replace("{IDRecurso}", id.ToString());
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
                ModelState.AddModelError(string.Empty, "Error al eliminar el evento .");
                return Page();
            }
        }
    }
}
