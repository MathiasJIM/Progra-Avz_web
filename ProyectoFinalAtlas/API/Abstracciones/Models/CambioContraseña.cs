using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Models
{
    public class CambioContrasena
    {
        public Guid UsuarioId { get; set; }
        public string NuevaContraseñaHash { get; set; }
        public string ConfirmarContrasena { get; set; }
    }

}
