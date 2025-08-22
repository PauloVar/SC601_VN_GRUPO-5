using System;
using System.Collections.Generic;

namespace Proyecto.ML.Entities;

public partial class Tarea
{
    public int IdTarea { get; set; }

    public int IdUsuario { get; set; }

    public string Descripcion { get; set; } = null!;

    public DateTime FechaHoraSolicitud { get; set; }

    public DateTime? FechaHoraUpdate { get; set; }

    public int CreadaPor { get; set; }

    public int? UpdatePor { get; set; }

    public int IdEstadoTarea { get; set; }

    public int IdPrioridad { get; set; }

    public virtual Usuario CreadaPorNavigation { get; set; } = null!;

    public virtual EstadoTarea IdEstadoTareaNavigation { get; set; } = null!;

    public virtual Prioridad IdPrioridadNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual Usuario? UpdatePorNavigation { get; set; }
}
