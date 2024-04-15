using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entities
{
    public class Recurso
    {
        
        public Guid ID { get; set; }


        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public string TipoRecurso { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public Guid? IDUsuarioCreacion { get; set; }

        public int? IDNivel { get; set; }

        public string LinkRefuerzo { get; set; }
    }
}
