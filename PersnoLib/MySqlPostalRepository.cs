using MySqlConnector;

namespace PersnoLib;


public class MySqlPostalRepository : IPostalRepository
{
  
    private readonly string _connectionString = "Server=localhost;Port=3306;Database=testdb;User=user;Password=rootpassword;SSL Mode=None";

    public MySqlPostalRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public MySqlPostalRepository()
    {
        
    }

    public string GetRandomPostalAndTown()
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            connection.Open();

            using (var command =
                   new MySqlCommand("SELECT `cPostalCode`, `cTownName` FROM `postal_code` ORDER BY RAND() LIMIT 1",
                       connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())  // Ensure you call Read() before accessing the result
                    {
                        return $"{reader.GetString("cPostalCode")} - {reader.GetString("cTownName")}";
                    }
                    else
                    {
                        throw new Exception("No data found.");
                    }
                }
            }
        }
    }
}