using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
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
