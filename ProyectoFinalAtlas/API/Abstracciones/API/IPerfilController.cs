using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IPerfilController
    {
        Task<IActionResult> ObtenerUsuarioId(Guid IDUsuario);
    }
}
