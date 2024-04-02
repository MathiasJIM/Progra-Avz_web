using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IUsuarioBW
    {
        Task<Guid> RegistrarUsuario(RegistroUsuario usuario);
    }
}
