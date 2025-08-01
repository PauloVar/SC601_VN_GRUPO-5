using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public partial class Prioridad
{
    public int IdPrioridad { get; set; }

    public string Prioridad1 { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
