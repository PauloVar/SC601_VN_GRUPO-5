using Proyecto.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.DAL.UnitsOfWork
{
    public interface IUnitOfWork : IDisposable
    {

        ITareaRepository Tareas { get; }
        IPrioridadRepository Prioridades { get; }
        IEstadoTareaRepository EstadoTareas { get; }
        IUsuarioRepository Usuarios { get; }
        Task<int> SaveChangesAsync();
    }
}
