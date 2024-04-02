using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BC
{
    public interface IUsuarioBC
    {
        Task<Guid> RegistrarUsuario(Abstracciones.Models.RegistroUsuario usuario);
    }
}
