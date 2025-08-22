using Proyecto.DAL.Interfaces;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.Interfaces
{
    public interface ITareaRepository : IGenericRepository<Tarea>
    {
        //Task<int> GetMaxId();
        Task<IEnumerable<Tarea>> GetAllWithRelations();

        Task<Tarea?> GetById(int id);

        Task<Tarea?> GetByIdWithIncludes(int id);
    }

    
}
