using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPA_G5.Data
{
    public class ProyectoPADbContext : IdentityDbContext

    {

        public ProyectoPADbContext(DbContextOptions<ProyectoPADbContext> options)
                    : base(options)
        {
        }

        public DbSet<Proyecto.ML.Entities.Usuario> Usuarios { get; set; }


        public DbSet<Proyecto.ML.Entities.Estado> Estados { get; set; }

    }

}
