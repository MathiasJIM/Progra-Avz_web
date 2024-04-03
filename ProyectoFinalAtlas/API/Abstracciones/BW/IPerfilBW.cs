using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IPerfilBW
    {
        Task<Abstracciones.Models.Perfil> GerPerfilPorIdUser(Guid IDUsuario);

        Task<Guid> CrearPerfil(Perfil perfil, Guid IDUsuario);
        Task<int> ActualizarPerfilPorUsuarioId(Guid usuarioId, Perfil perfil);
    }
}
