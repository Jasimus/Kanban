using System.ComponentModel.DataAnnotations;

namespace Tablero_space;

public class Tablero
{
    [Required]
    int id;

    [Required]
    int idUsuarioPropietario;

    [Required]
    string nombre;
    string? descripcion;

    public int Id { get => id; set => id = value; }
    public int IdUsuarioPropietario { get => idUsuarioPropietario; set => idUsuarioPropietario = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string? Descripcion { get => descripcion; set => descripcion = value; }

    public Tablero(int id, int idUsuarioPropietario, string nombre, string? descripcion)
    {
        this.id = id;
        this.idUsuarioPropietario = idUsuarioPropietario;
        this.nombre = nombre;
        this.descripcion = descripcion;
    }

    public Tablero(){}
}