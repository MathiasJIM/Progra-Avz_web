using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Recurso
{
    public class MostrarRecursosModel : PageModel
    {
        private Configuracion _configuracion;

        public MostrarRecursosModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public List<Abstracciones.Modelos.Recurso> Recurso { get; set; } = new List<Abstracciones.Modelos.Recurso>();
        public string Tipo { get; set; }
        public string Mensaje { get; set; }

        public async Task<IActionResult> OnGetAsync(string tipo)
        {
            Tipo = tipo;

            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
            if (userIdClaim == null)
            {
                return NotFound();
            }

            var userId = userIdClaim.Value;
            string endPoint = _configuracion.GetEndpoint("MostrarRecursos") + $"?tipo={tipo}";
            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endPoint);
            if (!respuesta.IsSuccessStatusCode)
            {
                Mensaje = "Error al cargar recursos";
                return Page();
            }

            var contenido = await respuesta.Content.ReadAsStringAsync();
            Recurso = JsonConvert.DeserializeObject<List<Abstracciones.Modelos.Recurso>>(contenido);

            if (tipo != null)
            {
                Recurso = Recurso.Where(r => r.TipoRecurso.Equals(tipo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return Page();
        }

    }
}
