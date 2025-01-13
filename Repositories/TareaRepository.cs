namespace TareaRepository_space;
using Microsoft.Data.Sqlite;
using Tarea_space;

public interface ITareaRepository
{
    public void CrearTarea(int id, Tarea tarea);
    public void ModificarTarea(int id, Tarea tarea);
    public Tarea? DetallesTarea(int id);
    public List<Tarea> ListarTareasDeUsuario(int? id);
    public List<Tarea> ListarTareasDeTablero(int id);
    public void EliminarTarea(int id);
}


public class TareaRepository : ITareaRepository
{
    string _ConnectionString;

    public TareaRepository(string ConnectionString)
    {
        _ConnectionString = ConnectionString;
    }

    public void CrearTarea(int id, Tarea tarea)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Tarea(id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_tablero", id);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@estado", (int)tarea.Estado);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            command.Parameters.AddWithValue("@color", tarea.Color);
            command.Parameters.AddWithValue("@id_usuario_asignado", tarea.IdUsuarioAsignado);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }
        if(cant == 0)
            throw new Exception("No se creó la tarea");
    }

    public void ModificarTarea(int id, Tarea tarea)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_tablero", tarea.IdTablero);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@estado", (int)tarea.Estado);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            command.Parameters.AddWithValue("@color", tarea.Color);
            command.Parameters.AddWithValue("@id_usuario_asignado", tarea.IdUsuarioAsignado);
            command.Parameters.AddWithValue("@id", id);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        if(cant == 0)
            throw new Exception("No se modificó la tarea");
    }

    public Tarea? DetallesTarea(int id)
    {
        Tarea? tarea = null;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tarea WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    tarea = new Tarea(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_tablero"]), reader["nombre"].ToString(), reader["descripcion"].ToString(), reader["color"].ToString(), Convert.ToInt32(reader["estado"]), Convert.ToInt32(reader["id_usuario_asignado"]));
                }
            }
            connection.Close();
        }

        return tarea;
    }

    public List<Tarea> ListarTareasDeUsuario(int? id)
    {
        List<Tarea> tareas = new List<Tarea>();
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tarea WHERE id_usuario_asignado = @id_usuario;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_usuario", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var tarea = new Tarea(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_tablero"]), reader["nombre"].ToString(), reader["descripcion"].ToString(), reader["color"].ToString(), Convert.ToInt32(reader["estado"]), Convert.ToInt32(reader["id_usuario_asignado"]));

                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }

        return tareas;
    }

    public List<Tarea> ListarTareasDeTablero(int id)
    {
        List<Tarea> tareas = new List<Tarea>();
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tarea WHERE id_tablero = @id_tablero;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_tablero", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var tarea = new Tarea(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_tablero"]), reader["nombre"].ToString(), reader["descripcion"].ToString(), reader["color"].ToString(), Convert.ToInt32(reader["estado"]), Convert.ToInt32(reader["id_usuario_asignado"]));

                    tareas.Add(tarea);
                }
            }
            connection.Close();
        }

        return tareas;
    }

    public void EliminarTarea(int id)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "DELETE FROM Tarea WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            
            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        if(cant == 0)
            throw new Exception("No se modificó la tarea");
    }
}