namespace TareasController_space;

using Microsoft.AspNetCore.Mvc;
using TareaRepository_space;
using Tarea_space;
using UsuarioRepository_space;

public class TareasController : Controller
{
    readonly ITareaRepository _tareaRepository;
    readonly IUsuarioRepository _usuarioRepository;
    readonly IEstadoTareaColor _estadoTareaColor;

    public TareasController(ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository, IEstadoTareaColor estadoTareaColor)
    {
        _tareaRepository = tareaRepository;
        _usuarioRepository = usuarioRepository;
        _estadoTareaColor = estadoTareaColor;
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
        ViewBag.idTablero = idTablero;
        ViewBag.usuarios = _usuarioRepository.ListarUsuarios();
        return View();
    }

    [HttpPost]
    public IActionResult CrearTarea(Tarea tarea)
    {
        int cant = _tareaRepository.CrearTarea(tarea.IdTablero, tarea);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("DetalleTablero", new {id = tarea.IdTablero});
        ViewBag.error = "no se pudo crear la tarea";
        ViewBag.idTablero = tarea.IdTablero;
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

    public IActionResult DetalleTarea(int id)
    {
        Tarea tarea = _tareaRepository.DetallesTarea(id);
        ViewBag.usuario = _usuarioRepository.ListarUsuarios().FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
        ViewBag.EstadoTareaColor = _estadoTareaColor.ObtenerDiccionario();
        return View(tarea);
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