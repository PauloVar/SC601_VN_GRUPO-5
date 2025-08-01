using Proyecto.DAL.Context;
using Proyecto.DAL.Interfaces;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.Repositories
{
    public class EstadoTareaRepository : GenericRepository<EstadoTarea>, IEstadoTareaRepository
    {
        public EstadoTareaRepository(ProyectoTareasContext context) : base(context)
        {
        }
    }
}
