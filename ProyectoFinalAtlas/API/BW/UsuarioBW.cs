using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Models;

namespace BW
{
    public class UsuarioBW : IUsuarioBW
    {
        private IUsuarioBC _usuarioBC;
        private IUsuarioDA _usuarioDA;

        public UsuarioBW(IUsuarioBC usuarioBC, IUsuarioDA usuarioDA)
        {
            _usuarioBC = usuarioBC;
            _usuarioDA = usuarioDA;
        }

        public async Task<Guid> RegistrarUsuario(RegistroUsuario usuario)
        {
            return await _usuarioBC.RegistrarUsuario(usuario);
        }

        public async Task<int> ActualizarUsuarioPorId(Guid id, Abstracciones.Models.ActualizarUsuario usuario)
        {
           return await _usuarioDA.ActualizarUsuarioPorId(id, usuario);
        }

        public async Task<Abstracciones.Models.Usuario> ObtenerUsuarioPorId(Guid id)
        {
            return await _usuarioDA.ObtenerUsuarioPorId(id);
        }

        public async Task<EstadoCuenta> ObtenerEstadoPorId(int idEst)
        {
            return await _usuarioDA.ObtenerEstadoPorId(idEst);
        }

        public async Task<Rol> ObtenerNombreRolPorId(int idRol)
        {
            return await _usuarioDA.ObtenerNombreRolPorId(idRol);
        }
    }
}
