using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public class Estado
{
    public int IdEstado { get; set; }

    public string Estado1 { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
