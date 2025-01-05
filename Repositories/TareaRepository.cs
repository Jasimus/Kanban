namespace TareaRepository_space;
using Microsoft.Data.Sqlite;
using Tarea_space;
public class TareaRepository
{
    string connectionString = "Data Source=db/Kanban1.db;Cache=Shared;";

    public int CrearTarea(int id, Tarea tarea)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Tarea(id_tablero, nombre, estado, descripcion, color, id_usuario_asignado) VALUES (@id_tablero, @nombre, @estado, @descripcion, @color, @id_usuario_asignado);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_tablero", id);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@estado", tarea.Estado);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            command.Parameters.AddWithValue("@color", tarea.Color);
            command.Parameters.AddWithValue("@id_usuario_asignado", tarea.IdUsuarioAsignado);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }
        return cant;
    }

    public int ModificarTarea(int id, Tarea tarea)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "UPDATE Tarea SET id_tablero = @id_tablero, nombre = @nombre, estado = @estado, descripcion = @descripcion, color = @color, id_usuario_asignado = @id_usuario_asignado WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_tablero", tarea.IdTablero);
            command.Parameters.AddWithValue("@nombre", tarea.Nombre);
            command.Parameters.AddWithValue("@estado", tarea.Estado);
            command.Parameters.AddWithValue("@descripcion", tarea.Descripcion);
            command.Parameters.AddWithValue("@color", tarea.Color);
            command.Parameters.AddWithValue("@id_usuario_asignado", tarea.IdUsuarioAsignado);
            command.Parameters.AddWithValue("@id", id);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        return cant;
    }

    public Tarea DetallesTarea(int id)
    {
        Tarea tarea = null;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
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
        List<Tarea> tareas = null;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
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
        List<Tarea> tareas = null;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
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

    public int EliminarTarea(int id)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "DELETE FROM Tarea WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            
            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        return cant;
    }

    public int AsignarTarea(int idTarea, int idUsuario)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            connection.Open();
                
            string query = "UPDATE Tarea SET id_usuario_asignado = @id_usuario WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", idTarea);
            command.Parameters.AddWithValue("@id_usuario", idUsuario);
            
            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        return cant;
    }
}