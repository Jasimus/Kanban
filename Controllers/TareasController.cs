namespace TareasController_space;

using Microsoft.AspNetCore.Mvc;
using TareaRepository_space;
using Tarea_space;

public class TareasController : Controller
{
    TareaRepository tr = new TareaRepository();

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ListarTareaDeTablero(int id)
    {
        var tareas = tr.ListarTareasDeTablero(id);
        return View(tareas);
    }

    [HttpGet]
    public IActionResult ListarTareaDeUsuario()
    {
        int? id = HttpContext.Session.GetInt32("idUsuario");
        var tareas = tr.ListarTareasDeUsuario(id);
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
        int cant = tr.CrearTarea(idTablero, tarea);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo crear la tarea";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarTarea(int idTablero, int id)
    {
        var tarea = tr.DetallesTarea(id);
        ViewBag.idTablero = idTablero;
        return View(tarea);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int idTablero, Tarea tarea)
    {
        int cant = tr.ModificarTarea(tarea.Id, tarea);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo modificar la tarea";
        return View();
    }

    [HttpGet]
    public IActionResult EliminarTarea(int idTablero, int id)
    {
        var tarea = tr.DetallesTarea(id);
        ViewBag.idTablero = idTablero;
        return View(tarea);
    }

    [HttpPost]
    public IActionResult EliminarTarea(int idTablero, Tarea tarea)
    {
        int cant = tr.EliminarTarea(tarea.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTareasDeTablero", new {id = idTablero});
        ViewBag.error = "no se pudo eliminar la tarea";
        return View();
    }
}