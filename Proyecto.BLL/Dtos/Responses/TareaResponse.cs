using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Dtos.Responses
{
    public class TareaResponse
    {
        public int IdTarea { get; set; }
        public string Descripcion { get; set; } = null!;
        public string Prioridad { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public DateTime FechaHoraSolicitud { get; set; }

        public int IdPrioridad { get; set; }
        public int IdEstadoTarea { get; set; }
        public int CreadaPor { get; set; }
        public int IdUsuario { get; set; }

        // NUEVAS propiedades para los nombres
        public string NombreCreador { get; set; } = null!;
        public string NombreUsuarioAsignado { get; set; } = null!;
    }
}
