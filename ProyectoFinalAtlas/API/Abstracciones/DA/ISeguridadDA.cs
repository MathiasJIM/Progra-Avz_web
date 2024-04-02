using Abstracciones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.DA
{
    public interface ISeguridadDA
    {
        Task<Usuario> ObtenerUsuario(Usuario usuario);
    }
}
