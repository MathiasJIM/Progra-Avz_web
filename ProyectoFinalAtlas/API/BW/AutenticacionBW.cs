using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelos;

namespace BW
{
    public class AutenticacionBW : IAutenticacionBW
    {
        private IAutenticacionBC _autenticacionBC;
        private ISeguridadDA _seguridadDA;

        public AutenticacionBW(IAutenticacionBC autenticacionBC, ISeguridadDA seguridadDA)
        {
            _autenticacionBC = autenticacionBC;
            _seguridadDA = seguridadDA;
        }

        public Token Login(Login login)
        {
            return _autenticacionBC.Login(login);
        }

        public async Task<Abstracciones.Models.Usuario> ObtenerUsuario(Abstracciones.Models.Usuario usuario)
        {
            return await _seguridadDA.ObtenerUsuario(usuario);
        }

    }
}
