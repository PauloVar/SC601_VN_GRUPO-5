using Proyecto.ML.Entities;

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

    // ✅ Relaciones internas a este contexto
    public virtual EstadoTarea IdEstadoTareaNavigation { get; set; } = null!;
    public virtual Prioridad IdPrioridadNavigation { get; set; } = null!;

    // ❌ Se eliminan las propiedades de navegación hacia Usuario
    // ❌ porque Usuario vive en otro contexto
}