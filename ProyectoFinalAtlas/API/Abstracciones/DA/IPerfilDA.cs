using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IPerfilDA
    {
        Task<Abstracciones.Models.Perfil> GerPerfilPorIdUser(Guid IDUsuario);
        Task<Guid> CrearPerfil(Perfil perfil, Guid IDUsuario);

        Task<int> ActualizarPerfilPorUsuarioId(Guid usuarioId, Abstracciones.Models.Perfil perfil);
    }
}
