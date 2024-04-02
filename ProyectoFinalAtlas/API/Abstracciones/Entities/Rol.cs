using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entities
{
    public class Rol
    {
        public int ID { get; set; }
        public string NombreRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
