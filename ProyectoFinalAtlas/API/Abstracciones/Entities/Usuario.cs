using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Entities
{
    public class Usuario
    {
        public Guid ID { get; set; }
        public string NombreCompleto { get; set; }
        public string CorreoElectronico { get; set; }
        public int IDRol { get; set; }
        public int IDEstadoCuenta { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime UltimoInicioSesion { get; set; }
        public string ContrasenaHash { get; set; }
        public string ContrasenaTemporal { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual EstadoCuenta EstadoCuenta { get; set; }
    }
}
