using Proyecto.ML.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.DAL.Interfaces
{
    public interface IEstadoTareaRepository
    {
        Task<List<EstadoTarea>> GetAll();
    }
}
