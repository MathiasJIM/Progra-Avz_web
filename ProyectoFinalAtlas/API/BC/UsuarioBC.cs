using Abstracciones.BC;
using Abstracciones.DA;
using Abstracciones.Modelos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Abstracciones.BW;
using Abstracciones.Models;


namespace BC
{
    public class UsuarioBC : IUsuarioBC
    {
        public IConfiguration _configuration;

        private IUsuarioDA _usuarioDA;

        public UsuarioBC(IConfiguration configuration, IUsuarioDA usuarioDA)
        {
            _configuration = configuration;
            _usuarioDA = usuarioDA;
        }



        public async Task<Guid> RegistrarUsuario(RegistroUsuario usuario)
        {
            RegistroUsuario usuarioCompleto = new RegistroUsuario() { CorreoElectronico = usuario.CorreoElectronico, NombreCompleto = usuario.NombreCompleto, IDRol = usuario.IDRol };
            usuarioCompleto.ContrasenaHash = usuario.ContrasenaHash;
            return await _usuarioDA.RegistrarUsuario(usuarioCompleto);
        }
    }
}
