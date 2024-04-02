using System.ComponentModel.DataAnnotations;

namespace Abstracciones.Models
{
    public class RegistroUsuario
    {
        [Required(ErrorMessage = "El nombre completo es requerido")]
        public string NombreCompleto { get; set; }

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido")]
        public string CorreoElectronico { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string ContrasenaHash { get; set; }

        [Compare("ContrasenaHash", ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; }

        [Required(ErrorMessage = "El rol es requerido")]
        public int IDRol { get; set; }
    }
}
