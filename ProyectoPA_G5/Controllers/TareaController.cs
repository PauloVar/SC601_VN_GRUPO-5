using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto.BLL.Dtos.Responses;
using Proyecto.BLL.Interfaces;

namespace ProyectoPA_G5.Controllers
{
    public class TareaController : Controller
    {
        private readonly ITareaService _tareaService;

        public TareaController(ITareaService tareaService)
        {
            _tareaService = tareaService;
        }

        public async Task<IActionResult> Index()
        {
            var tareas = await _tareaService.GetAll();
            return View(tareas);
        }

        public IActionResult Create() => View();

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

            return View(tarea);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, TareaRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

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
