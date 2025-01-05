namespace LogueoController_space;

using Microsoft.AspNetCore.Mvc;
using UsuarioRepository_space;

public class LogueoController : Controller
{
    UsuarioRepository ur = new UsuarioRepository();

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        var usuario = ur.ListarUsuarios().FirstOrDefault(u => u.Nombre == username && u.Password == password);

        if(usuario == null) return View();

        HttpContext.Session.SetInt32("idUsuario", usuario.Id);
        HttpContext.Session.SetInt32("rol", (int)usuario.RolUsuario);
        HttpContext.Session.SetString("nombre", usuario.Nombre);

        return RedirectToAction("Index", "Home");
    }
}