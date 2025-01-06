namespace TableroRepository_space;

using Microsoft.Data.Sqlite;
using Tablero_space;


public interface ITableroRepository
{
    public int CrearTablero(Tablero tablero);
    public int ModificarTablero(int id, Tablero tablero);
    public Tablero DetallesTablero(int id);
    public List<Tablero> ListarTableros();
    public List<Tablero> ListarTablerosDeUsuario(int id);
    public int EliminarTablero(int id);

}



public class TableroRepository : ITableroRepository
{
    readonly string _ConnectionString;

    public TableroRepository(string ConnectionString)
    {
        _ConnectionString = ConnectionString;
    }
    public int CrearTablero(Tablero tablero)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "INSERT INTO Tablero(id_usuario_propietario, nombre, descripcion) VALUES (@id_usuario, @nombre, @descripcion);";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", tablero.Nombre);
            command.Parameters.AddWithValue("@id_usuario", tablero.IdUsuarioPropietario);
            command.Parameters.AddWithValue("@descripcion", tablero.Descripcion);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }
        return cant;
    }

    public int ModificarTablero(int id, Tablero tablero)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "UPDATE Tablero SET nombre = @nombre, id_usuario_propietario = @id_usuario, descripcion = @descripcion WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", tablero.Nombre);
            command.Parameters.AddWithValue("@id_usuario", tablero.IdUsuarioPropietario);
            command.Parameters.AddWithValue("@descripcion", tablero.Descripcion);
            command.Parameters.AddWithValue("@id", id);

            cant = command.ExecuteNonQuery();

            connection.Close();
        }

        return cant;
    }

    public Tablero DetallesTablero(int id)
    {
        Tablero tablero = null;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tablero WHERE id = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    tablero = new Tablero(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_usuario_propietario"]), reader["nombre"].ToString(), reader["descripcion"].ToString());
                }
            }
            connection.Close();
        }

        return tablero;
    }

    public List<Tablero> ListarTableros()
    {
        List<Tablero> tableros = new List<Tablero>();
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tablero;";

            SqliteCommand command = new SqliteCommand(query, connection);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var tablero = new Tablero(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_usuario_propietario"]), reader["nombre"].ToString(), reader["descripcion"].ToString());

                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }

        return tableros;
    }

    public List<Tablero> ListarTablerosDeUsuario(int id)
    {
        List<Tablero> tableros = new List<Tablero>();
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "SELECT * FROM Tablero WHERE id_usuario_propietario = @id_usuario;";

            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id_usuario", id);
            
            using (var reader = command.ExecuteReader())
            {
                while(reader.Read())
                {
                    var tablero = new Tablero(Convert.ToInt32(reader["id"]), Convert.ToInt32(reader["id_usuario_propietario"]), reader["nombre"].ToString(), reader["descripcion"].ToString());

                    tableros.Add(tablero);
                }
            }
            connection.Close();
        }

        return tableros;
    }

    public int EliminarTablero(int id)
    {
        int cant = 0, cant1 = 0;
        using (SqliteConnection connection = new SqliteConnection(_ConnectionString))
        {
            connection.Open();
                
            string query = "DELETE FROM Tablero WHERE id = @id;";
            string query1 = "SELECT * FROM Tarea WHERE id_tablero = @id;";

            SqliteCommand command = new SqliteCommand(query, connection);
            SqliteCommand command1 = new SqliteCommand(query1, connection);

            command.Parameters.AddWithValue("@id", id);
            command1.Parameters.AddWithValue("@id", id);
            
            using(var reader = command1.ExecuteReader())
            {
                while(reader.Read()) cant1++;
            }

            if(cant1 == 0)
            {
                cant = command.ExecuteNonQuery();
            }

            connection.Close();
        }

        return cant;
    }
}