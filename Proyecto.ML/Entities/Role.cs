using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public partial class Role
{
    public int Rolid { get; set; }

    public string Rol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
