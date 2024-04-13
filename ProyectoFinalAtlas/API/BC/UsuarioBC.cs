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
using MailKit.Net.Smtp;
using MimeKit;



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
            await EnviarContrasenaTemporalPorCorreo(usuario);
            return await _usuarioDA.RegistrarUsuario(usuarioCompleto);
        }

        public async Task EnviarContrasenaTemporalPorCorreo(RegistroUsuario usuario)
        {
            var email = _configuration.GetSection("EmailInfo:Mail");
            var contra = _configuration.GetSection("EmailInfo:Password");
            var mensaje = await CrearCorreo(usuario);

            using (var cliente = new SmtpClient())
            {
                await cliente.ConnectAsync("smtp.office365.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                await cliente.AuthenticateAsync(email.Value,contra.Value);
                await cliente.SendAsync(mensaje);
                await cliente.DisconnectAsync(true);
            }
        }

        public async Task<MimeMessage> CrearCorreo(RegistroUsuario usuario)
        {
            var email = _configuration.GetSection("EmailInfo:Mail");
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("IceCastle", email.Value));
            mensaje.To.Add(new MailboxAddress(usuario.NombreCompleto, usuario.CorreoElectronico));
            mensaje.Subject = "Contraseña Temporal";
            mensaje.Body = new TextPart("plain")
            {
                Text = $"Su contraseña temporal es: {usuario.ContrasenaTemporal}"
            };

            return mensaje;
        }
    }
}
