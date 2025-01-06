namespace LogueoController_space;

using Microsoft.AspNetCore.Mvc;
using UsuarioRepository_space;
using Usuario_space;
public class LogueoController : Controller
{
    readonly IUsuarioRepository _usuarioRepository;

    public LogueoController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        List<Usuario> usuarios = _usuarioRepository.ListarUsuarios();
        if(usuarios != null)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Nombre == username && u.Password == password);

            if(usuario == null)
            {
                ViewBag.error = "usuario no encontrado";
                return View();
            }

            HttpContext.Session.SetInt32("idUsuario", usuario.Id);
            HttpContext.Session.SetInt32("rol", (int)usuario.RolUsuario);
            HttpContext.Session.SetString("nombre", usuario.Nombre);

            return RedirectToAction("ListarUsuarios", "Usuarios");
        }
        ViewBag.error = "no existen usuarios";
        return View();
    }
}