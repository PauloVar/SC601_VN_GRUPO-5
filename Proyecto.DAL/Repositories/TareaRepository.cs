using Microsoft.EntityFrameworkCore;
using Proyecto.DAL.Context;
using Proyecto.DAL.Repositories;
using Proyecto.ML.Entities;
using Proyecto.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.Repositories
{
    public class TareaRepository : GenericRepository<Tarea>, ITareaRepository
    {
        public TareaRepository(ProyectoTareasContext context) : base(context)
        {
        }

        //public async Task<int> GetMaxId()
        //{
        //    return await _context.Clientes.MaxAsync(x => x.Identificacion);
        //}

        //public async Task<IEnumerable<Tarea>> GetAllWithRelations()
        //{
        //    return await _context.Tareas
        //        .Include(t => t.IdPrioridadNavigation)
        //        .Include(t => t.IdEstadoTareaNavigation)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Tarea>> GetAllWithRelations()
        {
            return await _context.Tareas
                .Include(t => t.IdPrioridadNavigation)
                .Include(t => t.IdEstadoTareaNavigation)
                .Include(t => t.IdUsuarioNavigation)       // Usuario asignado
                .Include(t => t.CreadaPorNavigation)       // Usuario que crea
                .ToListAsync();
        }


        public async Task<Tarea?> GetById(int id)
        {
            return await _context.Tareas
                .Include(t => t.IdEstadoTareaNavigation)
                .Include(t => t.IdPrioridadNavigation)
                .FirstOrDefaultAsync(t => t.IdTarea == id);
        }

        public async Task<Tarea?> GetByIdWithIncludes(int id)
        {
            return await _context.Tareas
                .Include(t => t.IdUsuarioNavigation)
                .Include(t => t.CreadaPorNavigation)
                .Include(t => t.IdPrioridadNavigation)
                .Include(t => t.IdEstadoTareaNavigation)
                .FirstOrDefaultAsync(t => t.IdTarea == id);
        }



    }
}
