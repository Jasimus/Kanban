namespace LogueoController_space;

using Microsoft.AspNetCore.Mvc;
using UsuarioRepository_space;
using Usuario_space;
public class LogueoController : Controller
{
    readonly IUsuarioRepository _usuarioRepository;
    readonly ILogger<LogueoController> _logger;

    public LogueoController(IUsuarioRepository usuarioRepository, ILogger<LogueoController> logger)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Login()
    {
        HttpContext.Session.Clear();
        return View();
    }


    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        try
        {
            List<Usuario> usuarios = _usuarioRepository.ListarUsuarios();
            Usuario? usuario = usuarios.FirstOrDefault(u => u.Nombre == username && u.Password == password);

            if(usuario == null)
            {
                _logger.LogWarning("Intento de acceso inválido - Usuario: " + username + " - Clave: " + password);
                ViewBag.error = "usuario no encontrado";
                return View();
            }

            HttpContext.Session.SetInt32("idUsuario", usuario.Id);
            HttpContext.Session.SetInt32("rol", (int)usuario.RolUsuario);
            HttpContext.Session.SetString("nombre", usuario.Nombre);

            _logger.LogInformation("El usuario "+ usuario.Nombre + " ingresó correctamente");

            return RedirectToAction("Index", "Usuarios");
        }
        catch(Exception ex)
        {
            _logger.LogError(ex.ToString());
            ViewBag.error = "no existen usuarios";
            return View();
        }

    }
}