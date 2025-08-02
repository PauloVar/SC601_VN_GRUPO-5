using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Dtos.Requests
{
    public class TareaRequest
    {
        public int IdUsuario { get; set; }
        [MinLength(30, ErrorMessage = "La descripción debe tener al menos 30 caracteres")]
        public string Descripcion { get; set; } = null!;
        public DateTime FechaHoraSolicitud { get; set; }
        public int IdEstadoTarea { get; set; }
        public int IdPrioridad { get; set; }
        public int CreadaPor { get; set; }

    }
}
