using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW
{
    public class PerfilBW : IPerfilBW
    {

        private IPerfilDA _perfilDA;

        public PerfilBW(IPerfilDA perfilDA)
        {
            _perfilDA = perfilDA;
        }
        public async Task<Abstracciones.Models.Perfil> GerPerfilPorIdUser(Guid IDUsuario)
        {
            return await _perfilDA.GerPerfilPorIdUser(IDUsuario);
        }

        public async Task<Guid> CrearPerfil(Perfil perfil, Guid IDUsuario)
        {
            return await _perfilDA.CrearPerfil(perfil, IDUsuario);
        }

        public async Task<int> ActualizarPerfilPorUsuarioId(Guid usuarioId, Abstracciones.Models.Perfil perfil)
        {
            return await _perfilDA.ActualizarPerfilPorUsuarioId(usuarioId, perfil);
        }
    }
}
