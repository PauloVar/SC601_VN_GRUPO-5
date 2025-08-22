using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto.ML.Entities;

namespace Proyecto.DAL.Context
{
    public class ProyectoPADbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public ProyectoPADbContext(DbContextOptions<ProyectoPADbContext> options)
            : base(options)
        { }

        // Otros DbSets propios
        public DbSet<Estado_db_first> Estados { get; set; }
    }
}


