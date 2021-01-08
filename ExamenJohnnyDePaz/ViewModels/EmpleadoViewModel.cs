using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenJohnnyDePaz.ViewModels
{
    public class EmpleadoViewModel
    {
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        [MaxLength(100, ErrorMessage = "Nombre excede los 100 caracteres permitidos")]
        public string NombreCompleto { get; set; }
        [Required(ErrorMessage = "DUI es requerido")]
        [MaxLength(10, ErrorMessage = "DUI excede los 10 caracteres permitidos")]
        public string Cedula { get; set; }
        [Required(ErrorMessage = "Correo es requerido")]
        [MaxLength(100, ErrorMessage = "Correo excede los 100 caracteres permitidos")]
        [RegularExpression(@"^[^@]+@[^@]+\.[a-zA-Z]{2,}$", ErrorMessage = "Correo no valido")]
        public string Correo { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        [Required(ErrorMessage = "Fecha de ingreso es requerida")]
        public DateTime FechaIngreso { get; set; }
        public int? IdJefe { get; set; }
        [Required]
        public int IdArea { get; set; }
        public IFormFile Foto { get; set; }
    }
}
