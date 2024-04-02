using System;
using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Modelos
{
    public class Usuario
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "El nombre completo es requerido")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public int IDRol { get; set; }

        public int IDEstadoCuenta { get; set; } = 0;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime UltimoInicioSesion { get; set; } = new DateTime(2000, 1, 1); // Otra fecha predeterminada

        public string ContrasenaHash { get; set; }
        public string ContrasenaTemporal { get; set; }

        public virtual Rol Rol { get; set; }
        public virtual EstadoCuenta EstadoCuenta { get; set; }
    }
}
