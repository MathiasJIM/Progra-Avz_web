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
        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string? Contrasenia { get; set; }
        public string? ContrasenaHash { get; set; }
        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 5 y 10", MinimumLength = 5)]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string? CorreoElectronico { get; set; }

    }
}
