using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.DAL.UnitsOfWork;
using Proyecto.ML.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proyecto.BLL.Servicios
{
    public class TareaService : ITareaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public TareaService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<Usuario> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        // Obtener todas las tareas con nombres de usuario asignado y creador
        public async Task<List<TareaResponse>> GetAll()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            var usuarioIds = tareas
                .SelectMany(t => new[] { t.IdUsuario, t.CreadaPor, t.UpdatePor ?? 0 })
                .Distinct()
                .Where(id => id != 0)
                .ToList();

            var usuarios = await _userManager.Users
                .Where(u => usuarioIds.Contains(u.Id))
                .ToListAsync();

            return tareas.Select(t =>
            {
                var usuarioAsignado = usuarios.FirstOrDefault(u => u.Id == t.IdUsuario);
                var creador = usuarios.FirstOrDefault(u => u.Id == t.CreadaPor);

                return new TareaResponse
                {
                    IdTarea = t.IdTarea,
                    Descripcion = t.Descripcion,
                    Prioridad = t.IdPrioridadNavigation?.Prioridad1 ?? "N/A",
                    Estado = t.IdEstadoTareaNavigation?.EstadoTarea1 ?? "N/A",
                    FechaHoraSolicitud = t.FechaHoraSolicitud,
                    IdUsuario = t.IdUsuario,
                    CreadaPor = t.CreadaPor,
                    NombreUsuarioAsignado = usuarioAsignado?.UserName ?? "N/A",
                    NombreCreador = creador?.UserName ?? "N/A"
                };
            }).ToList();
        }

        // Obtener tarea por id
        public async Task<TareaResponse?> GetById(int id)
        {
            var t = await _unitOfWork.Tareas.GetById(id);
            if (t == null) return null;

            var usuarioAsignado = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == t.IdUsuario);
            var creador = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == t.CreadaPor);

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
                NombreUsuarioAsignado = usuarioAsignado?.UserName ?? "N/A",
                NombreCreador = creador?.UserName ?? "N/A"
            };
        }

        // Crear tarea
        public async Task<bool> Create(TareaRequest request)
        {
            var tarea = _mapper.Map<Tarea>(request);
            await _unitOfWork.Tareas.Insert(tarea);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // Actualizar tarea
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

        // Borrar tarea
        public async Task<bool> Delete(int id)
        {
            var tarea = await _unitOfWork.Tareas.DeleteById(id);
            if (tarea == null) return false;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        // Obtener siguiente tarea pendiente
        public async Task<Tarea> ObtenerSiguienteTareaPendienteAsync()
        {
            var tareasPendientes = await _unitOfWork.Tareas.GetAllWithRelations();
            return tareasPendientes
                .Where(t => t.IdEstadoTarea == 1)
                .OrderByDescending(t => t.IdPrioridad)
                .ThenBy(t => t.FechaHoraSolicitud)
                .FirstOrDefault();
        }

        // Actualizar estado de tarea
        public async Task ActualizarEstadoAsync(Tarea tarea)
        {
            await _unitOfWork.Tareas.Update(tarea);
            await _unitOfWork.SaveChangesAsync();
        }

        // Obtener tareas en cola con prioridad y estado
        public async Task<IEnumerable<TareaResponse>> ObtenerTareasEnColaAsync()
        {
            var tareas = await _unitOfWork.Tareas.GetAllWithRelations();

            var estadosPermitidos = new[] { "Pendiente", "En Proceso" };
            var filtradas = tareas.Where(t => estadosPermitidos.Contains(t.IdEstadoTareaNavigation?.EstadoTarea1));

            string[] ordenPrioridad = new[] { "Alta", "Media", "Baja" };
            var ordenadas = filtradas.OrderBy(t => Array.IndexOf(ordenPrioridad, t.IdPrioridadNavigation?.Prioridad1 ?? "Baja"));

            var usuarioIds = ordenadas
                .SelectMany(t => new[] { t.IdUsuario, t.CreadaPor, t.UpdatePor ?? 0 })
                .Distinct()
                .Where(id => id != 0)
                .ToList();

            var usuarios = await _userManager.Users
                .Where(u => usuarioIds.Contains(u.Id))
                .ToListAsync();

            return ordenadas.Select(t =>
            {
                var usuarioAsignado = usuarios.FirstOrDefault(u => u.Id == t.IdUsuario);
                var creador = usuarios.FirstOrDefault(u => u.Id == t.CreadaPor);

                return new TareaResponse
                {
                    IdTarea = t.IdTarea,
                    Descripcion = t.Descripcion,
                    Prioridad = t.IdPrioridadNavigation?.Prioridad1 ?? "N/A",
                    Estado = t.IdEstadoTareaNavigation?.EstadoTarea1 ?? "N/A",
                    FechaHoraSolicitud = t.FechaHoraSolicitud,
                    NombreUsuarioAsignado = usuarioAsignado?.UserName ?? "N/A",
                    NombreCreador = creador?.UserName ?? "N/A"
                };
            });


        }
        public async Task<List<Prioridad>> GetPrioridades()
        {
            // Usamos UnitOfWork para traer todas las prioridades
            return await _unitOfWork.Prioridades.GetAll();
        }

        public async Task<List<EstadoTarea>> GetEstados()
        {
            // Usamos UnitOfWork para traer todos los estados
            return await _unitOfWork.EstadoTareas.GetAll();
        }
    }
}
