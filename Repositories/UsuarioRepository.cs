namespace UsuarioRepository_space;
using Microsoft.Data.Sqlite;
using Usuario_space;

public interface IUsuarioRepository
{
    public void CrearUsuario(Usuario usuario);
    public void ModificarUsuario(int id, Usuario usuario);
    public List<Usuario> ListarUsuarios();
    public Usuario? DetallesUsuario(int id);
    public void EliminarUsuario(int id);
    public void CambiarPassword(int id, string password);
}

public class UsuarioRepository : IUsuarioRepository
{
    string _ConnectionString;

    public UsuarioRepository(string ConnectionString)
    {
        _ConnectionString = ConnectionString;
    }

    public void CrearUsuario(Usuario usuario)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Usuario(nombre, password, rolusuario) VALUES (@nombre, @password, @rol);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@password", usuario.Password);
            command.Parameters.AddWithValue("@rol", usuario.RolUsuario);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }
        
        if(cant == 0)
            throw new Exception("No se creó el usuario");
    }

    public void ModificarUsuario(int id, Usuario usuario)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "UPDATE Usuario SET nombre = @nombre, password = @password, rolusuario = @rol WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", usuario.Nombre);
            command.Parameters.AddWithValue("@password", usuario.Password);
            command.Parameters.AddWithValue("@rol", (int)usuario.RolUsuario);
            command.Parameters.AddWithValue("@id", id);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        if(cant == 0)
            throw new Exception("No se modificó el usuario");
    }

    public List<Usuario> ListarUsuarios()
    {
        List<Usuario> usuarios = new List<Usuario>();
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Usuario;";

            SqliteCommand command = new SqliteCommand(query, connection);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var usuario = new Usuario(Convert.ToInt32(reader["id"]), reader["nombre"].ToString(), reader["password"].ToString(), Convert.ToInt32(reader["rolusuario"]));

                    usuarios.Add(usuario);
                }
            }
            connection.Close();
        }

        if(usuarios.Count == 0)
            throw new Exception("No hay usuarios");

        return usuarios;
    }

    public Usuario? DetallesUsuario(int id)
    {
        Usuario? usuario = null;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Usuario WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    usuario = new Usuario(Convert.ToInt32(reader["id"]), reader["nombre"].ToString(), reader["password"].ToString(), Convert.ToInt32(reader["rolusuario"]));

                }
            }
            connection.Close();
        }

        if(usuario == null)
            throw new Exception("Usuario no encontrado");

        return usuario;
    }

    public void EliminarUsuario(int id)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query1 = "DELETE FROM Tarea WHERE id_usuario_asignado = @id;";
            string query = "DELETE FROM Usuario WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteCommand command1 = new SqliteCommand(query1, connection);
            command.Parameters.AddWithValue("@id", id);
            command1.Parameters.AddWithValue("@id", id);
            
            command1.ExecuteNonQuery();
            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        if(cant == 0)
            throw new Exception("No se eliminó el usuario");
    }

    public void CambiarPassword(int id, string password)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "UPDATE Usuario SET password = @password WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@id", id);
            
            cant = command.ExecuteNonQuery();
            connection.Close();
        }

        if(cant == 0)
            throw new Exception("No se cambió el password");
    }
}