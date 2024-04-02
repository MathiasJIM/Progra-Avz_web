using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entities
{
    public class EstadoCuenta
    {
        public int ID { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
