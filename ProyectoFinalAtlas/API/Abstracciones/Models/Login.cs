using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Login
    {
        [Required(ErrorMessage = "El usaurio es requerido")]
        [StringLength(20, ErrorMessage = "El tamaño debe ser de entre 5 y 20", MinimumLength = 5)]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string ContrasenaHash { get; set; }

    }
}
