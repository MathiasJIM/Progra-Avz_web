﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Token
    {
        public bool ValidacionExitosa { get; set; }
        public string? AccessToken { get; set; }
    }
}
