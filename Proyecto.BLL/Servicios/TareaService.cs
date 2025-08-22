using AutoMapper;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.DAL.UnitsOfWork;
using Proyecto.ML.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoPA_G5.Data;

namespace Proyecto.BLL.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper; // si usás AutoMapper
        private readonly ProyectoPADbContext _usuariosContext;

        public TareaService(IUnitOfWork unitOfWork, IMapper mapper, ProyectoPADbContext usuariosContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _usuariosContext = usuariosContext;
        }

        public async Task<List<TareaResponse>> GetAll()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            // Obtener IDs de usuario
            var usuarioIds = tareas
                .SelectMany(t => new[] { t.IdUsuario, t.CreadaPor, t.UpdatePor ?? 0 })
                .Distinct()
                .Where(id => id != 0)
                .ToList();

            var usuarios = await _usuariosContext.Usuarios
                .Where(u => usuarioIds.Contains(u.Id_Usuario))
                .ToListAsync();

            return tareas.Select(t =>
            {
                var usuarioAsignado = usuarios.FirstOrDefault(u => u.Id_Usuario == t.IdUsuario);
                var creador = usuarios.FirstOrDefault(u => u.Id_Usuario == t.CreadaPor);

                return new TareaResponse
                {
                    IdTarea = t.IdTarea,
                    Descripcion = t.Descripcion,
                    Prioridad = t.IdPrioridadNavigation.Prioridad1,
                    Estado = t.IdEstadoTareaNavigation.EstadoTarea1,
                    FechaHoraSolicitud = t.FechaHoraSolicitud,
                    IdUsuario = t.IdUsuario,
                    CreadaPor = t.CreadaPor,
                    NombreUsuarioAsignado = usuarioAsignado?.Nombre ?? "N/A",
                    NombreCreador = creador?.Nombre ?? "N/A"
                };
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

        public async Task<Tarea> ObtenerSiguienteTareaPendienteAsync()
        {
            var tareasPendientes = await _unitOfWork.Tareas.GetAllWithRelations();


            return tareasPendientes
                .Where(t => t.IdEstadoTarea == 1)
                .OrderByDescending(t => t.IdPrioridad)
                .ThenBy(t => t.FechaHoraSolicitud)
                .FirstOrDefault();
        }

        public async Task ActualizarEstadoAsync(Tarea tarea)
        {
            await _unitOfWork.Tareas.Update(tarea);
            await _unitOfWork.SaveChangesAsync();
        }


        public async Task<TareaResponse?> GetById(int id)
        {
            var t = await _unitOfWork.Tareas.GetById(id);
            if (t == null) return null;

            var usuarioAsignado = await _usuariosContext.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == t.IdUsuario);
            var creador = await _usuariosContext.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario == t.CreadaPor);

            return new TareaResponse
            {
                IdTarea = t.IdTarea,
                Descripcion = t.Descripcion,
                Estado = t.IdEstadoTareaNavigation?.EstadoTarea1 ?? "N/A",
                Prioridad = t.IdPrioridadNavigation?.Prioridad1 ?? "N/A",
                FechaHoraSolicitud = t.FechaHoraSolicitud,
                IdEstadoTarea = t.IdEstadoTarea,
                IdPrioridad = t.IdPrioridad,
                IdUsuario = t.IdUsuario,
                CreadaPor = t.CreadaPor,
                NombreUsuarioAsignado = usuarioAsignado?.Nombre ?? "N/A",
                NombreCreador = creador?.Nombre ?? "N/A"
            };
        }
        public async Task<IEnumerable<TareaResponse>> ObtenerTareasEnColaAsync()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            var estadosPermitidos = new[] { "Pendiente", "En Proceso" };
            var filtradas = tareas.Where(t => estadosPermitidos.Contains(t.IdEstadoTareaNavigation.EstadoTarea1));

            string[] ordenPrioridad = new[] { "Alta", "Media", "Baja" };
            var ordenadas = filtradas.OrderBy(t => Array.IndexOf(ordenPrioridad, t.IdPrioridadNavigation.Prioridad1));

            var usuarioIds = ordenadas
                .SelectMany(t => new[] { t.IdUsuario, t.CreadaPor, t.UpdatePor ?? 0 })
                .Distinct()
                .Where(id => id != 0)
                .ToList();

            var usuarios = await _usuariosContext.Usuarios
                .Where(u => usuarioIds.Contains(u.Id_Usuario))
                .ToListAsync();

            return ordenadas.Select(t =>
            {
                var usuarioAsignado = usuarios.FirstOrDefault(u => u.Id_Usuario == t.IdUsuario);
                var creador = usuarios.FirstOrDefault(u => u.Id_Usuario == t.CreadaPor);

                return new TareaResponse
                {
                    IdTarea = t.IdTarea,
                    Descripcion = t.Descripcion,
                    Prioridad = t.IdPrioridadNavigation.Prioridad1,
                    Estado = t.IdEstadoTareaNavigation.EstadoTarea1,
                    FechaHoraSolicitud = t.FechaHoraSolicitud,
                    NombreUsuarioAsignado = usuarioAsignado?.Nombre ?? "N/A",
                    NombreCreador = creador?.Nombre ?? "N/A"
                };
            });
        }




    }
}
