using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Interfaces
{
    public interface ITareaService
    {
        Task<List<TareaResponse>> GetAll();
        Task<TareaResponse?> GetById(int id);
        Task<bool> Create(TareaRequest request);
        Task<bool> Update(int id, TareaRequest request);
        Task<bool> Delete(int id);

        Task<Tarea> ObtenerSiguienteTareaPendienteAsync();
        Task ActualizarEstadoAsync(Tarea tarea);

        Task<List<Prioridad>> GetPrioridades();
        Task<List<EstadoTarea>> GetEstados();


        Task<IEnumerable<TareaResponse>> ObtenerTareasEnColaAsync();
    }
}
