using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Web.Helpers;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        [BindProperty]
        public string resultado { get; set; }
        private Configuracion _configuracion;
        public IndexModel(ILogger<IndexModel> logger, Configuracion configuracion)
        {
            _logger = logger;
            _configuracion = configuracion;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated)
            {
                string endPoint = _configuracion.GetEndpoint("Validar");
                var cliente = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
                request.Headers.Add("Authorization", $"Bearer {User.Identities.First().Claims.Where(c => c.Type == "Token").FirstOrDefault().Value}");
                var response = await cliente.SendAsync(request);
                response.EnsureSuccessStatusCode();
                resultado = await response.Content.ReadAsStringAsync();
                return Page();
            }
            return RedirectToPage("/Acceso/Login");
        }





    }
}
