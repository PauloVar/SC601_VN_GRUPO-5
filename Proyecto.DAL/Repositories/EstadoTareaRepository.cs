using Microsoft.EntityFrameworkCore;
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
    public class EstadoTareaRepository : IEstadoTareaRepository
    {
        private readonly ProyectoTareasContext _context;

        public EstadoTareaRepository(ProyectoTareasContext context)
        {
            _context = context;
        }

        public async Task<List<EstadoTarea>> GetAll()
        {
            return await _context.EstadoTareas.ToListAsync();
        }
    }
}
