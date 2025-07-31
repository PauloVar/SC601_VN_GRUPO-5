using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.ML.Entities
{
    public class Estado
    {
        [Key]
        public int Id_Estado { get; set; }

        [Required]
        [MaxLength(20)]
        public string EstadoNombre { get; set; }
    }
}
