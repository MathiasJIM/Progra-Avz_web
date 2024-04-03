using Abstracciones.Modelos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using Web.Helpers;

namespace Web.Pages.Acceso
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LoginInfo { get; set; } = default!;
        [BindProperty]
        public Token token { get; set; }
        private Configuracion _configuracion;

        public LoginModel(Configuracion configuracion)
        {
            this._configuracion = configuracion;
        }

        public void OnGet()
        {
        }




        public async Task<IActionResult> OnPostAsync()
        {
            var Hash = Password.GenerarHash(LoginInfo.Contrasenia);
            LoginInfo.ContrasenaHash = Password.ObtenerHash(Hash);
            LoginInfo.CorreoElectronico = LoginInfo.CorreoElectronico;

            string endPoint = _configuracion.GetEndpoint("Login");
            var cliente = new HttpClient();
            var respuesta = await cliente.PostAsJsonAsync(string.Format(endPoint), LoginInfo);
            respuesta.EnsureSuccessStatusCode();
            token = JsonConvert.DeserializeObject<Token>(respuesta.Content.ReadAsStringAsync().Result);

            if (token.ValidacionExitosa)
            {
                // Llamar a tu API para obtener información adicional del usuario
                var infoUsuarioResponse = await ObtenerInformacionUsuario(LoginInfo.CorreoElectronico);

                if (infoUsuarioResponse.IsSuccessStatusCode)
                {
                    var usuarioJson = await infoUsuarioResponse.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<Abstracciones.Modelos.Usuario>(usuarioJson);

                    JwtSecurityToken? tokens = leerInformacionToken();
                    await AgregarClaims(tokens, usuario);
                    return RedirectToPage("/Index");
                }
            }
            return Page();
        }

        private async Task<HttpResponseMessage> ObtenerInformacionUsuario(string correoElectronico)
        {
            string endPoint = _configuracion.GetEndpoint("ObtenerUsuario");
            var cliente = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint + "?correoElectronico=" + correoElectronico);
            var response = await cliente.SendAsync(request);
            return response;
        }

        private async Task AgregarClaims(JwtSecurityToken? tokens, Abstracciones.Modelos.Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.CorreoElectronico),
                new Claim(ClaimTypes.Role,usuario.IDRol.ToString()),
                new Claim("Token", token.AccessToken),
                new Claim("Id", usuario.ID.ToString())
            };

            await establecerAutenticacion(claims);
        }


        private JwtSecurityToken? leerInformacionToken()
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.AccessToken);
            var tokens = jsonToken as JwtSecurityToken;
            return tokens;
        }



        private async Task establecerAutenticacion(List<Claim> claims)
        {
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);
        }
    }
}
