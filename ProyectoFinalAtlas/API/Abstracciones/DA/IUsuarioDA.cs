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
    }
}
