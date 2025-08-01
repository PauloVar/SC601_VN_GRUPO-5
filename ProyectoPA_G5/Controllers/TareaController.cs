using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.BLL.Dtos.Requests;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.DAL.UnitsOfWork;
using AutoMapper;

namespace ProyectoPA_G5.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;


        public TareaController(ITareaService tareaService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _tareaService = tareaService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var tareas = await _tareaService.GetAll();
            return View(tareas);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Prioridades = new SelectList(await _unitOfWork.Prioridades.GetAll(), "IdPrioridad", "Prioridad1");
            ViewBag.Estados = new SelectList(await _unitOfWork.EstadoTareas.GetAll(), "IdEstadoTarea", "EstadoTarea1");
            ViewBag.Usuarios = new SelectList(await _unitOfWork.Usuarios.GetAll(), "IdUsuario", "Nombre");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TareaRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _tareaService.Create(request);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null)
                return NotFound();

            // Mapea tarea a TareaRequest 
            var request = new TareaRequest
            {
                IdUsuario = tarea.IdUsuario,          
                Descripcion = tarea.Descripcion,
                IdPrioridad = tarea.IdPrioridad,
                IdEstadoTarea = tarea.IdEstadoTarea,
                CreadaPor = tarea.CreadaPor,
                FechaHoraSolicitud = tarea.FechaHoraSolicitud
            };

            // Cargar de datos 
            ViewBag.Prioridades = new SelectList(await _unitOfWork.Prioridades.GetAll(), "IdPrioridad", "Prioridad1");
            ViewBag.Estados = new SelectList(await _unitOfWork.EstadoTareas.GetAll(), "IdEstadoTarea", "EstadoTarea1");
            ViewBag.Usuarios = new SelectList(await _unitOfWork.Usuarios.GetAll(), "IdUsuario", "Nombre");

            return View(request);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id, TareaRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Si falla validación, recarga los combos y retorna la vista
                ViewBag.Prioridades = new SelectList(await _unitOfWork.Prioridades.GetAll(), "IdPrioridad", "Prioridad1");
                ViewBag.Estados = new SelectList(await _unitOfWork.EstadoTareas.GetAll(), "IdEstadoTarea", "EstadoTarea1");
                ViewBag.Usuarios = new SelectList(await _unitOfWork.Usuarios.GetAll(), "IdUsuario", "Nombre");
                return View(request);
            }

            await _tareaService.Update(id, request);
            return RedirectToAction("Index");
        }


        //public async Task<IActionResult> Delete(int id)
        //{
        //    var tarea = await _tareaService.GetById(id);
        //    if (tarea == null)
        //        return NotFound();

        //    return View(tarea);
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    await _tareaService.Delete(id);
        //    return RedirectToAction("Index");
        //}

        // GET: Tarea/Details/5
        // Controlador Details
        public async Task<IActionResult> Details(int id)
        {
            var tareaResponse = await _tareaService.GetById(id);
            if (tareaResponse == null) return NotFound();
            // Ya tienes los nombres en el DTO:
            ViewBag.NombreUsuarioAsignado = tareaResponse.NombreUsuarioAsignado;
            ViewBag.NombreCreador = tareaResponse.NombreCreador;
            return View(tareaResponse);
        }

        // GET: Tarea/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var tareaResponse = await _tareaService.GetById(id);
            if (tareaResponse == null)
            {
                return NotFound();
            }

            // Ya tienes los nombres en el DTO:
            ViewBag.NombreUsuarioAsignado = tareaResponse.NombreUsuarioAsignado;
            ViewBag.NombreCreador = tareaResponse.NombreCreador;

            return View(tareaResponse);
        }

        // POST: Tarea/DeleteConfirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eliminado = await _tareaService.Delete(id);
            if (!eliminado)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ColaTareas()
        {
            var tareasEnCola = await _tareaService.ObtenerTareasEnColaAsync();

            var tareasAgrupadas = tareasEnCola
                .GroupBy(t => t.Prioridad)
                .OrderBy(g => Array.IndexOf(new[] { "Alta", "Media", "Baja" }, g.Key));

            return View(tareasAgrupadas);
        }




    }
}
