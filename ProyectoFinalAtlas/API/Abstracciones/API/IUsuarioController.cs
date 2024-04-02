﻿using Abstracciones.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.API
{
    public interface IUsuarioController
    {
        Task<IActionResult> PostAsync([FromBody] RegistroUsuario usuario);
    }
}
