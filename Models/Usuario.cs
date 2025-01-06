using System.ComponentModel.DataAnnotations;

namespace Usuario_space;

public class Usuario
{
    int id;

    [Required]
    string nombre;

    [Required]
    string password;
    RolUsuario rolUsuario;

    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Password { get => password; set => password = value; }
    public RolUsuario RolUsuario {get => rolUsuario; set => rolUsuario = value; }

    public Usuario(int id, string nombre, string password, int rol)
    {
        this.id = id;
        this.nombre = nombre;
        this.password = password;
        rolUsuario = (RolUsuario)rol;
    }

    public Usuario(){}
}

public enum RolUsuario
{
    Administrador = 1,
    Operador = 2
}

