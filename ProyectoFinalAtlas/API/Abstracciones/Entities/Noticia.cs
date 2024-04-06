using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entities
{
    public class Noticia
    {
        public Guid ID { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string imagen { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public Guid IDUsuarioPublico { get; set; }

    }
}
