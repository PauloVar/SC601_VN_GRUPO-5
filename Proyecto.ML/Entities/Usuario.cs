using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public int Rolid { get; set; }

    public int IdEstado { get; set; }

    public virtual Estado IdEstadoNavigation { get; set; } = null!;

    public virtual Role Rol { get; set; } = null!;

    public virtual ICollection<Tarea> TareaCreadaPorNavigations { get; set; } = new List<Tarea>();

    public virtual ICollection<Tarea> TareaIdUsuarioNavigations { get; set; } = new List<Tarea>();

    public virtual ICollection<Tarea> TareaUpdatePorNavigations { get; set; } = new List<Tarea>();
}
