using AutoMapper;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.DAL.UnitsOfWork;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.BLL.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // si usás AutoMapper

        public TareaService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TareaResponse>> GetAll()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            return tareas.Select(t => new TareaResponse
            {
                IdTarea = t.IdTarea,
                Descripcion = t.Descripcion,
                Prioridad = t.IdPrioridadNavigation.Prioridad1,
                Estado = t.IdEstadoTareaNavigation.EstadoTarea1,
                FechaHoraSolicitud = t.FechaHoraSolicitud
            }).ToList();
        }

        public async Task<bool> Create(TareaRequest request)
        {
            var tarea = _mapper.Map<Tarea>(request);
            await _unitOfWork.Tareas.Insert(tarea);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, TareaRequest request)
        {
            var tarea = await _unitOfWork.Tareas.GetById(id);
            if (tarea == null) return false;

            tarea.Descripcion = request.Descripcion;
            tarea.IdPrioridad = request.IdPrioridad;
            tarea.IdEstadoTarea = request.IdEstadoTarea;

            await _unitOfWork.Tareas.Update(tarea);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var tarea = await _unitOfWork.Tareas.DeleteById(id);
            if (tarea == null)
                return false;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }



        public async Task<TareaResponse?> GetById(int id)
        {
            var t = await _unitOfWork.Tareas.GetByIdWithIncludes(id);
            if (t == null) return null;

            return new TareaResponse
            {
                IdTarea = t.IdTarea,
                Descripcion = t.Descripcion,
                Estado = t.IdEstadoTareaNavigation?.EstadoTarea1 ?? "N/A",
                Prioridad = t.IdPrioridadNavigation?.Prioridad1 ?? "N/A",
                FechaHoraSolicitud = t.FechaHoraSolicitud,
                IdEstadoTarea = t.IdEstadoTarea,
                IdPrioridad = t.IdPrioridad,
                CreadaPor = t.CreadaPor,
                IdUsuario = t.IdUsuario,
                // Aquí puedes agregar nombres si quieres en el DTO:
                NombreCreador = t.CreadaPorNavigation?.Nombre ?? "N/A",
                NombreUsuarioAsignado = t.IdUsuarioNavigation?.Nombre ?? "N/A"
            };
        }
        public async Task<IEnumerable<TareaResponse>> ObtenerTareasEnColaAsync()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            // Asegurarse que los estados coincidan exactamente con los valores de la base
            var estadosPermitidos = new[] { "Pendiente", "En Proceso" }; // corregido a "En Proceso"

            var filtradas = tareas.Where(t => estadosPermitidos.Contains(t.IdEstadoTareaNavigation.EstadoTarea1));

            // Ordenar según prioridad definida en la base (mayúsculas iniciales)
            string[] ordenPrioridad = new[] { "Alta", "Media", "Baja" };

            var ordenadas = filtradas.OrderBy(t => Array.IndexOf(ordenPrioridad, t.IdPrioridadNavigation.Prioridad1));

            return ordenadas.Select(t => new TareaResponse
            {
                IdTarea = t.IdTarea,
                Descripcion = t.Descripcion,
                Prioridad = t.IdPrioridadNavigation.Prioridad1,
                Estado = t.IdEstadoTareaNavigation.EstadoTarea1,
                FechaHoraSolicitud = t.FechaHoraSolicitud,
                NombreCreador = t.CreadaPorNavigation?.Nombre ?? "N/A",
                NombreUsuarioAsignado = t.IdUsuarioNavigation?.Nombre ?? "N/A"
            });
        }




    }
}
