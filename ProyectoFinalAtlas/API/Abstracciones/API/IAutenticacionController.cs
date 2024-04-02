using Abstracciones.Modelos;
using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IAutenticacionController
    {
        IActionResult Post([FromBody] Login loginRequest);

        IActionResult GetValidarAutenticacion();

        Task<ActionResult<Usuario>> ObtenerUsuarioPorCorreo(string correoElectronico);

    }
}
