namespace UsuarioController_space;

using Microsoft.AspNetCore.Mvc;
using Usuario_space;
using UsuarioRepository_space;
using TableroRepository_space;
using TareaRepository_space;
using IndexVM_space;

public class UsuariosController : Controller
{
    readonly IUsuarioRepository _usuarioRepository;
    readonly ITableroRepository _tableroRepository;
    readonly ITareaRepository _tareaRepository;
    readonly ILogger<UsuariosController> _logger;

    public UsuariosController(IUsuarioRepository usuarioRepository, ITableroRepository tableroRepository, ITareaRepository tareaRepository, ILogger<UsuariosController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _tableroRepository = tableroRepository;
        _tareaRepository = tareaRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index()
    {
        int id = (int)HttpContext.Session.GetInt32("idUsuario");
        var indexVM = new IndexVM
        {
            Tableros = _tableroRepository.ListarTablerosDeUsuario(id),
            Tareas = _tareaRepository.ListarTareasDeUsuario(id),
            Usuarios = _usuarioRepository.ListarUsuarios()
        };

        return View(indexVM);
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        try
        {
            return View(_usuarioRepository.ListarUsuarios());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            return BadRequest();
        }
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        try
        {
            _usuarioRepository.CrearUsuario(usuario);
            if (ModelState.IsValid)
            return RedirectToAction("ListarUsuarios", "Usuarios");
            ViewBag.error = "no se pudo crear el usuario";
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo crear el usuario";
            return View();
        }
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = _usuarioRepository.DetallesUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {
        try
        {
            _usuarioRepository.ModificarUsuario(usuario.Id, usuario);
            if (ModelState.IsValid)
            return RedirectToAction("ListarUsuarios", "Usuarios");
            ViewBag.error = "no se pudo modificar el usuario";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo modificar el usuario";
            return View();
        }
    }

    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        var usuario = _usuarioRepository.DetallesUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        try
        {
            _usuarioRepository.EliminarUsuario(usuario.Id);
            if (ModelState.IsValid)
            return RedirectToAction("ListarUsuarios", "Usuarios");
            ViewBag.error = "no se pudo eliminar el usuario";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo eliminar el usuario";
            return View();
        }
    }

    [HttpGet]
    public IActionResult CambiarPasswordAntes()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CambiarPasswordAntes(string usuario)
    {
        try
        {
            Usuario Uusuario = _usuarioRepository.ListarUsuarios().Single(u => u.Nombre == usuario);
            return RedirectToAction("CambiarPassword", "Usuarios", new {id = Uusuario.Id});
        }
        catch(InvalidOperationException ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "el usuario no existe o coincide con otro usuario";
            return View();
        }
    }


    [HttpGet]
    [Route("Usuarios/CambiarPassword")]
    public IActionResult CambiarPassword(int id)
    {
        return View(id);
    }

    [HttpPost]
    [Route("Usuarios/CambiarPasswordPost")]
    public IActionResult CambiarPassword(int id, string password)
    {
        try
        {
            _usuarioRepository.CambiarPassword(id, password);
            if (ModelState.IsValid)
            return RedirectToAction("Login", "Logueo");
            ViewBag.error = "no se pudo cambiar la contraseña";
            return View();
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no se pudo cambiar la contraseña";
            return View();
        }
    }
}