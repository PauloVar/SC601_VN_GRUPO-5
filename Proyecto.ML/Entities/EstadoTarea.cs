using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public partial class EstadoTarea
{
    public int IdEstadoTarea { get; set; }

    public string EstadoTarea1 { get; set; } = null!;

    public virtual ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
