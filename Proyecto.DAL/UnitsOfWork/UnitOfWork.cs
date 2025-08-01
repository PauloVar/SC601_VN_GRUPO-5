using Proyecto.DAL.Context;
using Proyecto.DAL.Interfaces;
using Proyecto.DAL.Repositories;
using Proyecto.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProyectoTareasContext _context;

        public UnitOfWork(ProyectoTareasContext context)
        {
            _context = context;
        }

        private ITareaRepository _tareas;
        private IPrioridadRepository _prioridades;
        private IEstadoTareaRepository _estadoTareas;

        public ITareaRepository Tareas =>
            _tareas ?? new TareaRepository(_context);

        public IPrioridadRepository Prioridades =>
            _prioridades ?? new PrioridadRepository(_context);

        public IEstadoTareaRepository EstadoTareas =>
            _estadoTareas ?? new EstadoTareaRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}
