using Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.BW
{
    public interface IAutenticacionBW
    {
        Token Login(Login login);

        Task<Abstracciones.Models.Usuario> ObtenerUsuario(Abstracciones.Models.Usuario usuario);
    }
}
