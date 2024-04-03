using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface IUsuarioDA
    {
        Task<Guid> RegistrarUsuario(Abstracciones.Models.RegistroUsuario usuario);
        Task<int> ActualizarUsuarioPorId(Guid id, Abstracciones.Models.ActualizarUsuario usuario);
        Task<Abstracciones.Models.Usuario> ObtenerUsuarioPorId(Guid id);

        Task<Rol> ObtenerNombreRolPorId(int idRol);
        Task<EstadoCuenta> ObtenerEstadoPorId(int idEst);
    }
}
