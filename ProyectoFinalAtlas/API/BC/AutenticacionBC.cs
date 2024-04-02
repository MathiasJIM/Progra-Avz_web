using Abstracciones.BC;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BC
{
    public class AutenticacionBC : IAutenticacionBC
    {
        public IConfiguration _configuration;

        public AutenticacionBC(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token Login(Login login)
        {
            Token repuestaToken = new Token() { AccessToken = string.Empty, ValidacionExitosa = false };
            TokenConfiguracion tokenConfiguracion = _configuration.GetSection("Token").Get<TokenConfiguracion>();
            //Metodo de validacion de credenciales
            if (login is null)
                return repuestaToken;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfiguracion.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim("correo", login.CorreoElectronico));

            var token = new JwtSecurityToken(tokenConfiguracion.Issuer,
              tokenConfiguracion.Issuer,
              claims,
              expires: DateTime.Now.AddMinutes(tokenConfiguracion.Expires),
              signingCredentials: credentials);


            repuestaToken.AccessToken = new JwtSecurityTokenHandler().WriteToken(token);
            repuestaToken.ValidacionExitosa = true;
            return repuestaToken;
        }

    }
}
