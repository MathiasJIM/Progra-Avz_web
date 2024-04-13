using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class CambioContrasena
    {
        public Guid UsuarioId { get; set; }
        public string NuevaContraseñaHash { get; set; }

        [Compare("NuevaContraseñaHash", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; }
    }

}
