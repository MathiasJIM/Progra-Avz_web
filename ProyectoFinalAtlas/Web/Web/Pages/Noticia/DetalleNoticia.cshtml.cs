using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Helpers;
using NoticiaModel = Abstracciones.Modelos.Noticia;

namespace Web.Pages.Noticia
{
    public class DetalleNoticiaModel : PageModel
    {
        private readonly Configuracion _configuracion;
        public DetalleNoticiaModel(Configuracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public NoticiaModel Noticia { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid IDNoticia)
        {
            if (IDNoticia == Guid.Empty)
            {
                return NotFound();
            }

            string endPoint = _configuracion.GetEndpoint("ObtenerNoticiaPorId").Replace("{IDNoticia}", IDNoticia.ToString());
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                Noticia = JsonConvert.DeserializeObject<NoticiaModel>(contenido);
                return Page();
            }

            return NotFound();
        }
    }
}
