using System;
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
        public string AccessToken { get; set; }
    }
    public class TokenConfiguracion
    {
        [Required(ErrorMessage = "El usaurio es requerido")]
        [StringLength(100, ErrorMessage = "El tamaño debe ser de entre 32 y 100", MinimumLength = 32)]
        public string Key { get; set; }
        [Required(ErrorMessage = "El usuario es requerido")]
        public string Issuer { get; set; }
        [Required(ErrorMessage = "El usuario es requerido")]
        public double Expires { get; set; }
    }
}
