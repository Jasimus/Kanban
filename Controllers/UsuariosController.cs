namespace UsuarioController_space;

using Microsoft.AspNetCore.Mvc;
using Usuario_space;
using UsuarioRepository_space;

public class UsuariosController : Controller
{
    readonly IUsuarioRepository _usuarioRepository;

    public UsuariosController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarUsuarios");
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        return View(_usuarioRepository.ListarUsuarios());
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        int cant = _usuarioRepository.CrearUsuario(usuario);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo crear el usuario";
        return View();
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
        int cant = _usuarioRepository.ModificarUsuario(usuario.Id, usuario);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo modificar el usuario";
        return View();
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
        int cant = _usuarioRepository.EliminarUsuario(usuario.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo eliminar el usuario";
        return View();
    }

    [HttpGet]
    public IActionResult CambiarPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CambiarPassword(int id, string password)
    {
        int cant = _usuarioRepository.CambiarPassword(id, password);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("Login", "Logueo");
        ViewBag.error = "no se pudo cambiar la contraseña";
        return View();
    }
}