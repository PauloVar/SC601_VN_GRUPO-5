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
            var deleted = await _unitOfWork.Tareas.DeleteById(id);
            if (deleted == null) return false;
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<TareaResponse?> GetById(int id)
        {
            var t = await _unitOfWork.Tareas.GetById(id);
            if (t == null) return null;

            return new TareaResponse
            {
                IdTarea = t.IdTarea,
                Descripcion = t.Descripcion,
                Estado = t.IdEstadoTareaNavigation.EstadoTarea1,
                Prioridad = t.IdPrioridadNavigation.Prioridad1,
                FechaHoraSolicitud = t.FechaHoraSolicitud
            };
        }
    }
}
