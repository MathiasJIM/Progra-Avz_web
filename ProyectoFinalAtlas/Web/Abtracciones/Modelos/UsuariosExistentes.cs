using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos
{
    public class UsuariosExistentes
    { 
            public Guid ID { get; set; }
            public string NombreCompleto { get; set; }
            public string CorreoElectronico { get; set; }
            public int? IDRol { get; set; }
            public string NombreRol { get; set; } 
            public int? IDEstadoCuenta { get; set; }
            public string EstadoCuentaDescripcion { get; set; } 
            public DateTime? FechaRegistro { get; set; }
            public DateTime? UltimoInicioSesion { get; set; }
            public string ContrasenaHash { get; set; }
            public string ContrasenaTemporal { get; set; }
        
    }
}
