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
    readonly IEstadoTareaColor _estadoTareaColor;
    readonly ILogger<TablerosController> _logger;

    public TablerosController(ITableroRepository tableroRepository, ITareaRepository tareaRepository, IEstadoTareaColor estadoTareaColor, ILogger<TablerosController> logger)
    {
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
        _estadoTareaColor = estadoTareaColor;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarTableros");
    }

    [HttpGet]
    public IActionResult ListarTableros()
    {
        try
        {
            int? idUsuario = HttpContext.Session.GetInt32("idUsuario");
            var tablerosU = _tableroRepository.ListarTablerosDeUsuario(idUsuario);
            var tablerosO = _tableroRepository.ListarTablerosDeOtros(idUsuario);
            var EstadoTareaColor = _estadoTareaColor.ObtenerDiccionario();

            foreach(Tarea t in _tareaRepository.ListarTareasDeUsuario(idUsuario))
            {
                foreach(Tablero tab in tablerosO)
                {
                    if(t.IdTablero == tab.Id) tablerosU.Add(tab);
                }
            }

            List<TableroVM> tablerosVM = new List<TableroVM>();

            foreach(Tablero tab in tablerosU)
            {
                var tableroVM = new TableroVM{
                    Tablero = tab,
                    Ideas = _tareaRepository.ListarTareasDeTablero(tab.Id).Where(t => t.Estado == EstadoTarea.Ideas).ToList(),
                    ToDo = _tareaRepository.ListarTareasDeTablero(tab.Id).Where(t => t.Estado == EstadoTarea.ToDo).ToList(),
                    Doing = _tareaRepository.ListarTareasDeTablero(tab.Id).Where(t => t.Estado == EstadoTarea.Doing).ToList(),
                    Review = _tareaRepository.ListarTareasDeTablero(tab.Id).Where(t => t.Estado == EstadoTarea.Review).ToList(),
                    Done = _tareaRepository.ListarTareasDeTablero(tab.Id).Where(t => t.Estado == EstadoTarea.Done).ToList()
                };
                tablerosVM.Add(tableroVM);
            }

            ViewBag.EstadoTareaColor = EstadoTareaColor;

            return View(tablerosVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult CrearTablero()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearTablero(Tablero tablero)
    {
        try
        {
            _tableroRepository.CrearTablero(tablero);
            if (ModelState.IsValid)
            return RedirectToAction("Index", "Usuarios");
            ViewBag.error = "no se pudo crear el tablero";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo crear el tablero";
            return View();
        }
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
        try
        {
            _tableroRepository.ModificarTablero(tablero.Id, tablero);
            if (ModelState.IsValid)
            return RedirectToAction("ListarTableros", "Tableros");
            ViewBag.error = "no se pudo modificar el tablero";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo modificar el tablero";
            return View();
        }
    }

    [HttpGet]
    public IActionResult DetalleTablero(int id)
    {
        try
        {
            var tablero = _tableroRepository.DetallesTablero(id);
            var tableroVM = new TableroVM{
                EstadoTareaColor = _estadoTareaColor.ObtenerDiccionario(),
                Tablero = tablero,
                Ideas = _tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Ideas).ToList(),
                ToDo = _tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.ToDo).ToList(),
                Doing = _tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Doing).ToList(),
                Review = _tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Review).ToList(),
                Done = _tareaRepository.ListarTareasDeTablero(id).Where(t => t.Estado == EstadoTarea.Done).ToList()
            };

            return View(tableroVM);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
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
        try
        {
            _tableroRepository.EliminarTablero(tablero.Id);
            if (ModelState.IsValid)
            return RedirectToAction("Index", "Usuarios");
            ViewBag.error = "no se pudo eliminar el tablero";
            return View(tablero);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo eliminar el tablero";
            return View(tablero);
        }
    }
}
