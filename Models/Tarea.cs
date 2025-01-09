namespace Tarea_space;

public class Tarea
{
    int id;
    int idTablero;
    string nombre;
    string descripcion;
    string color;
    EstadoTarea estado;
    int? idUsuarioAsignado;

    public int Id { get => id; set => id = value; }
    public int IdTablero { get => idTablero; set => idTablero = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Color { get => color; set => color = value; }
    public int? IdUsuarioAsignado { get => idUsuarioAsignado; set => idUsuarioAsignado = value; }
    public EstadoTarea Estado { get => estado; set => estado = value; }

    public Tarea(int id, int idTablero, string nombre, string descripcion, string color, int estado, int? idUsuarioAsignado)
    {
        this.id = id;
        this.idTablero = idTablero;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.color = color;
        this.estado = (EstadoTarea)estado;
        this.idUsuarioAsignado = idUsuarioAsignado;
    }

    public Tarea(){}
}

public enum EstadoTarea
{
    Ideas = 1,
    ToDo = 2,
    Doing = 3,
    Review = 4,
    Done = 5
}

public interface IEstadoTareaColor
{
    public Dictionary<int, string> ObtenerDiccionario();
}

public class EstadoTareaColor : IEstadoTareaColor
{
    public Dictionary<int, string> _diccionario;

    public EstadoTareaColor()
    {
        _diccionario = new Dictionary<int, string>
        {
            {1, "#82f0f5"},
            {2, "#5d78df"},
            {3, "#b6df5d"},
            {4, "#df5dbf"},
            {5, "#5dbfdf"}
        };
    }

    public Dictionary<int, string> ObtenerDiccionario() => _diccionario;
}