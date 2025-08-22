using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.ML.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ProyectoPA_G5.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;

        public TareaController(ITareaService tareaService, IMapper mapper, UserManager<Usuario> userManager)
        {
            _tareaService = tareaService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var tareas = await _tareaService.GetAll();
            return View(tareas);
        }

        public async Task<IActionResult> Create()
        {
            await CargarDropdowns(esEdicion: false);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TareaRequest request)
        {
            if (!ModelState.IsValid)
            {
                await CargarDropdowns(esEdicion: false);
                return View(request);
            }

            await _tareaService.Create(request);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null) return NotFound();

            var request = new TareaRequest
            {
                IdUsuario = tarea.IdUsuario,
                Descripcion = tarea.Descripcion,
                IdPrioridad = tarea.IdPrioridad,
                IdEstadoTarea = tarea.IdEstadoTarea,
                CreadaPor = tarea.CreadaPor,
                FechaHoraSolicitud = tarea.FechaHoraSolicitud
            };

            await CargarDropdowns(tarea.IdEstadoTarea, esEdicion: true);
            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TareaRequest request)
        {
            if (!ModelState.IsValid)
            {
                await CargarDropdowns(request.IdEstadoTarea, esEdicion: true);
                return View(request);
            }

            await _tareaService.Update(id, request);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null) return NotFound();
            return View(tarea);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null) return NotFound();
            return View(tarea);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eliminado = await _tareaService.Delete(id);
            if (!eliminado) return NotFound();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ColaTareas()
        {
            var tareasEnCola = await _tareaService.ObtenerTareasEnColaAsync();
            var tareasAgrupadas = tareasEnCola
                .GroupBy(t => t.Prioridad)
                .OrderBy(g => Array.IndexOf(new[] { "Alta", "Media", "Baja" }, g.Key));

            return View(tareasAgrupadas);
        }

        // ================= Helper =================
        private async Task CargarDropdowns(int? estadoActualId = null, bool esEdicion = false)
        {
            // Prioridades
            var prioridades = await _tareaService.GetPrioridades();
            ViewBag.Prioridades = new SelectList(prioridades, "IdPrioridad", "Prioridad1");

            // Estados
            var estados = await _tareaService.GetEstados();

            if (!esEdicion)
            {
                // Crear: solo Pendiente y En Proceso
                estados = estados.Where(e => e.EstadoTarea1 == "Pendiente").ToList();
            }
            else if (estadoActualId.HasValue)
            {
                // Editar: si estado es Cancelada, solo mostrar cancelada
                var estadoActual = estados.FirstOrDefault(e => e.IdEstadoTarea == estadoActualId.Value);
                if (estadoActual != null && estadoActual.EstadoTarea1 == "Cancelada")
                {
                    estados = estados.Where(e => e.EstadoTarea1 == "Cancelada").ToList();
                }
            }

            ViewBag.Estados = new SelectList(estados, "IdEstadoTarea", "EstadoTarea1");

            // Usuarios desde Identity
            var usuarios = await _userManager.Users.ToListAsync();
            ViewBag.Usuarios = new SelectList(usuarios, "Id", "UserName");
        }
    }
}
