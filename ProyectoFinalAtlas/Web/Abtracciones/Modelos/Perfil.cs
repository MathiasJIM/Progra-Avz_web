using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Perfil
    {

        public Guid ID { get; set; }

        public Guid IDUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Nombre { get; set; }

        [StringLength(255)]
        public string NombrePadreEncargado { get; set; }

        [StringLength(255)]
        public string ContactoEmergencia { get; set; }

        [StringLength(20)]
        public string NumeroTelefono { get; set; }

        public DateTime FechaNacimiento { get; set; }

        [StringLength(255)]
        public string FotoPerfil { get; set; } = "/images/default.png";



    }
}
