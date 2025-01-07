namespace IndexVM_space;

using Tablero_space;
using Tarea_space;
using Usuario_space;

public class IndexVM
{
    public List<Tablero> Tableros { get; set; }
    public List<Tarea> Tareas { get; set; }
    public List<Usuario> Usuarios { get; set; }
}