namespace TableroVM_space;

using Tablero_space;
using Tarea_space;

public class TableroVM
{
    public List<Tarea> Ideas { get; set; }
    public List<Tarea> ToDo { get; set; }
    public List<Tarea> Doing { get; set; }
    public List<Tarea> Review { get; set; }
    public List<Tarea> Done { get; set; }
    public Tablero Tablero { get; set; }
}