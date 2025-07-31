using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto.ML.Entities
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public DateTime Fecha_Registro { get; set; } = DateTime.Now;

        public int Id_Estado { get; set; }

        [ForeignKey(nameof(Id_Estado))]
        public Estado Estado { get; set; }
    }
}
