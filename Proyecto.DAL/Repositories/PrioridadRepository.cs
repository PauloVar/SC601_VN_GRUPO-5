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
    public class PrioridadRepository : GenericRepository<Prioridad>, IPrioridadRepository
    {
        public PrioridadRepository(ProyectoTareasContext context) : base(context)
        {
        }
    }
}
