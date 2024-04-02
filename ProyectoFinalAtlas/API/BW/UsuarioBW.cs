using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Models;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioBC _usuarioBC;

        public UsuarioBW(IUsuarioBC usuarioBC)
        {
            _usuarioBC = usuarioBC;
        }

        public async Task<Guid> RegistrarUsuario(RegistroUsuario usuario)
        {
            return await _usuarioBC.RegistrarUsuario(usuario);
        }
    }
}
