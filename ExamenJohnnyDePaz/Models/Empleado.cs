using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExamenJohnnyDePaz.Models
{
    public partial class Empleado
    {
        public Empleado()
        {
            EmpleadoHabilidad = new HashSet<EmpleadoHabilidad>();
            InverseIdJefeNavigation = new HashSet<Empleado>();
        }

        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(100,ErrorMessage ="Nombre excede los 100 caracteres permitidos")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "DUI es requerido")]
        [MaxLength(10, ErrorMessage = "DUI excede los 10 caracteres permitidos")]
        [RegularExpression(@"^[0-9]{8}-[0-9]{1}$", ErrorMessage = "DUI no válido")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Correo es requerido")]
        [MaxLength(100, ErrorMessage = "Correo excede los 100 caracteres permitidos")]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "Correo no válido")]
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Fecha de ingreso es requerida")]
        public DateTime FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        [Required]
        public int IdArea { get; set; }
        public byte[] Foto { get; set; }

        public virtual Area IdAreaNavigation { get; set; }
        public virtual Empleado IdJefeNavigation { get; set; }
        public virtual ICollection<EmpleadoHabilidad> EmpleadoHabilidad { get; set; }
        public virtual ICollection<Empleado> InverseIdJefeNavigation { get; set; }
    }
}
