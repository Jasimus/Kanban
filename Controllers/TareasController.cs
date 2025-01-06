namespace TareasController_space;

using Microsoft.AspNetCore.Mvc;
using TareaRepository_space;
using Tarea_space;

public class TareasController : Controller
{
    readonly ITareaRepository _tareaRepository;

    public TareasController(ITareaRepository tareaRepository)
    {
        _tareaRepository = tareaRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ListarTareaDeTablero(int id)
    {
        var tareas = _tareaRepository.ListarTareasDeTablero(id);
        return View(tareas);
    }

    [HttpGet]
    public IActionResult ListarTareaDeUsuario()
    {
        int? id = HttpContext.Session.GetInt32("idUsuario");
        var tareas = _tareaRepository.ListarTareasDeUsuario(id);
        return View(tareas);
    }

    [HttpGet]
    public IActionResult CrearTarea(int idTablero)
    {
        return View(idTablero);
    }

    [HttpPost]
    public IActionResult CrearTarea(int idTablero, Tarea tarea)
    {
        int cant = _tareaRepository.CrearTarea(idTablero, tarea);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo crear la tarea";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTablero, int id)
    {
        var tarea = _tareaRepository.DetallesTarea(id);
        ViewBag.idTablero = idTablero;
        return View(tarea);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int idTablero, Tarea tarea)
    {
        int cant = _tareaRepository.ModificarTarea(tarea.Id, tarea);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo modificar la tarea";
        return View();
    }

    [HttpGet]
    public IActionResult EliminarTarea(int idTablero, int id)
    {
        var tarea = _tareaRepository.DetallesTarea(id);
        ViewBag.idTablero = idTablero;
        return View(tarea);
    }

    [HttpPost]
    public IActionResult EliminarTarea(int idTablero, Tarea tarea)
    {
        int cant = _tareaRepository.EliminarTarea(tarea.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo eliminar la tarea";
        return View();
    }
}