using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;
using Proyecto.DAL.UnitsOfWork;

namespace ProyectoPA_G5.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;
        private readonly IUnitOfWork _unitOfWork;

        public TareaController(ITareaService tareaService, IUnitOfWork unitOfWork)
        {
            _tareaService = tareaService;
            _unitOfWork = unitOfWork;
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


        public async Task<IActionResult> Delete(int id)
        {
            var tarea = await _tareaService.GetById(id);
            if (tarea == null)
                return NotFound();

            return View(tarea);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _tareaService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
