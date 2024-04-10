using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Models
{
    public class Evento
    {
        public Guid ID { get; set; }

        [StringLength(255)]
        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime? FechaHoraInicio { get; set; }

        public int? Duracion { get; set; }
        public string Ubicacion { get; set; }

        public string TipoEvento { get; set; }

        public int? Concurrencia { get; set; }

        public Guid? IDUsuarioCreacion { get; set; }

        public DateTime? FechaHoraFinal { get; set; }
    }
}
