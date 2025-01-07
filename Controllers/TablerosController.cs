namespace TablerosController_space;

using Microsoft.AspNetCore.Mvc;
using TableroRepository_space;
using Tablero_space;
using Tarea_space;
using TableroVM_space;
using TareaRepository_space;

public class TablerosController : Controller
{
    readonly ITableroRepository _tableroRepository;
    readonly ITareaRepository _tareaRepository;

    public TablerosController(ITableroRepository tableroRepository, ITareaRepository tareaRepository)
    {
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarTableros");
    }

    [HttpGet]
    public IActionResult ListarTableros()
    {
        var tableros = _tableroRepository.ListarTableros();
        return View(tableros);
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        int cant = _tableroRepository.CrearTablero(tablero);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo crear el tablero";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = _tableroRepository.DetallesTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero)
    {
        int cant = _tableroRepository.ModificarTablero(tablero.Id, tablero);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo modificar el tablero";
        return View();
    }

    [HttpGet]
    public IActionResult DetalleTablero(int id)
    {
        var tablero = _tableroRepository.DetallesTablero(id);
        var tableroVM = new TableroVM{
            Tablero = tablero,
            Ideas = (List<Tarea>)_tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Ideas),
            ToDo = (List<Tarea>)_tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.ToDo),
            Doing = (List<Tarea>)_tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Doing),
            Review = (List<Tarea>)_tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Review),
            Done = (List<Tarea>)_tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Done)

        };

        return View(tableroVM);
    }

    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        var tablero = _tableroRepository.DetallesTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        int cant = _tableroRepository.EliminarTablero(tablero.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo eliminar el tablero";
        return View();
    }
}
