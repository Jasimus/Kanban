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
    readonly ILogger<TareasController> _logger;

    public TareasController(ITareaRepository tareaRepository, IUsuarioRepository usuarioRepository, IEstadoTareaColor estadoTareaColor, ILogger<TareasController> logger)
    {
        _tareaRepository = tareaRepository;
        _usuarioRepository = usuarioRepository;
        _estadoTareaColor = estadoTareaColor;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult ListarTareaDeTablero(int id)
    {
        try
        {
            var tareas = _tareaRepository.ListarTareasDeTablero(id);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult ListarTareaDeUsuario()
    {
        try
        {
            int? id = HttpContext.Session.GetInt32("idUsuario");
            var tareas = _tareaRepository.ListarTareasDeUsuario(id);
            return View(tareas);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
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
        try
        {
            Console.WriteLine((int)tarea.Estado);
            _tareaRepository.CrearTarea(tarea.IdTablero, tarea);
            if (ModelState.IsValid)
            return RedirectToAction("DetalleTablero", "Tableros", new {id = tarea.IdTablero});
            ViewBag.error = "no se pudo crear la tarea";
            ViewBag.idTablero = tarea.IdTablero;
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo crear la tarea";
            ViewBag.idTablero = tarea.IdTablero;
            return View();
        }
    }

    [HttpGet]
    public IActionResult ModificarTarea(int id)
    {
        var tarea = _tareaRepository.DetallesTarea(id);
        return View(tarea);
    }

    [HttpPost]
    public IActionResult ModificarTarea(int idTablero, Tarea tarea)
    {
        try
        {
            _tareaRepository.ModificarTarea(tarea.Id, tarea);
            if (ModelState.IsValid)
            return RedirectToAction("DetalleTablero", "Tableros", new {id = idTablero});
            ViewBag.error = "no se pudo modificar la tarea";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo modificar la tarea";
            return View();
        }
        
    }

    [HttpGet]
    public IActionResult DetalleTarea(int id)
    {
        try
        {
            Tarea tarea = _tareaRepository.DetallesTarea(id);
            ViewBag.usuario = _usuarioRepository.ListarUsuarios().FirstOrDefault(u => u.Id == tarea.IdUsuarioAsignado);
            ViewBag.EstadoTareaColor = _estadoTareaColor.ObtenerDiccionario();
            return View(tarea);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("Tareas/EliminarTarea")]
    public IActionResult EliminarTarea(int id)
    {
        var tarea = _tareaRepository.DetallesTarea(id);
        return View(tarea);
    }

    [HttpPost]
    [Route("Tareas/EliminarTareaPost")]
    public IActionResult EliminarTarea(Tarea tarea)
    {
        try
        {
            _tareaRepository.EliminarTarea(tarea.Id);
            if (ModelState.IsValid)
            return RedirectToAction("DetalleTablero", "Tableros", new {id = tarea.IdTablero});
            ViewBag.error = "no se pudo eliminar la tarea";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo eliminar la tarea";
            return View();
        }
    }
}