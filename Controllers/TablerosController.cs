namespace TablerosController_space;

using Microsoft.AspNetCore.Mvc;
using TableroRepository_space;
using Tablero_space;

public class TablerosController : Controller
{
    TableroRepository tr = new TableroRepository();

    [HttpGet]
    public IActionResult Index()
    {
        return View("ListarTableros");
    }

    [HttpGet]
    public IActionResult ListarTableros()
    {
        var tableros = tr.ListarTableros();
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
        int cant = tr.CrearTablero(tablero);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo crear el tablero";
        return View();
    }

    [HttpGet]
    public IActionResult ModificarTablero(int id)
    {
        var tablero = tr.DetallesTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult ModificarTablero(Tablero tablero)
    {
        int cant = tr.ModificarTablero(tablero.Id, tablero);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo modificar el tablero";
        return View();
    }

    [HttpGet]
    public IActionResult EliminarTablero(int id)
    {
        var tablero = tr.DetallesTablero(id);
        return View(tablero);
    }

    [HttpPost]
    public IActionResult EliminarTablero(Tablero tablero)
    {
        int cant = tr.EliminarTablero(tablero.Id);
        if (ModelState.IsValid && cant != 0)
        return RedirectToAction("ListarTableros", "Tableros");
        ViewBag.error = "no se pudo eliminar el tablero";
        return View();
    }
}
