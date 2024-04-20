using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class Asistencia
    {
        public Guid AsistenciaId { get; set; }
        public Guid EventoId { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime FechaHora { get; set; }
    }

}
