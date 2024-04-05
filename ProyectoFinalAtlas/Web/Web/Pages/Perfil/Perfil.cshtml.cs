using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages.Perfil
{
    public class PerfilModel : PageModel
    {
        private Configuracion _configuracion;


        public PerfilModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        [BindProperty]
        public Abstracciones.Modelos.Perfil Perfil { get; set; }

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
            if (Perfil == null)
            {
                return RedirectToPage("CrearPerfil");
            }

            return Page();
        }
        
        
    }
}
