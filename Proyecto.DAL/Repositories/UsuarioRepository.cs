using Proyecto.DAL.Context;
using Proyecto.DAL.Interfaces;
using Proyecto.ML.Entities;

namespace Proyecto.DAL.Repositories
{
    public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ProyectoTareasContext context) : base(context)
        {
        }
    }
}
