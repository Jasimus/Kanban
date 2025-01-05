namespace UsuarioController_space;

using Microsoft.AspNetCore.Mvc;
using Usuario_space;
using UsuarioRepository_space;

public class UsuariosController : Controller
{
    UsuarioRepository ur = new UsuarioRepository();

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarUsuarios");
    }

    [HttpGet]
    public IActionResult ListarUsuarios()
    {
        return View(ur.ListarUsuarios());
    }

    [HttpGet]
    public IActionResult CrearUsuario()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CrearUsuario(Usuario usuario)
    {
        int cant = ur.CrearUsuario(usuario);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo crear el usuario";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarUsuario(int id)
    {
        var usuario = ur.DetallesUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult ModificarUsuario(Usuario usuario)
    {
        int cant = ur.ModificarUsuario(usuario.Id, usuario);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo modificar el usuario";
        return View();
    }

    [HttpGet]
    public IActionResult EliminarUsuario(int id)
    {
        var usuario = ur.DetallesUsuario(id);
        return View(usuario);
    }

    [HttpPost]
    public IActionResult EliminarUsuario(Usuario usuario)
    {
        int cant = ur.EliminarUsuario(usuario.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarUsuarios", "Usuarios");
        ViewBag.error = "no se pudo eliminar el usuario";
        return View();
    }
}