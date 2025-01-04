namespace TableroRepository_space;
using Microsoft.Data.Sqlite;
using Tablero_space;
public class TaberoRepository
{
    string connectionString = "Data Source=db/Kanban1.db;Cache=Shared;";
    public int CrearTablero(Tablero tablero)
    {
        int cant = 0;
        using (SqliteConnection connection = new SqliteConnection(connectionString))
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
}