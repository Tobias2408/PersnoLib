using System.Text;
using MySqlConnector;

namespace PersnoLib;


public class Address
{
    private string _connectionString = "server=127.0.0.1;port=3306;database=addresses;user=root;password=your_password";

    private static Random _rand = new Random();
    public string Street { get; set; }
    public string Number { get; set; }
    public string Floor { get; set; }
    public string Door { get; set; }
    public string PostalCode { get; set; }
    public string Town { get; set; }

    // Constructor to initialize Address
    public Address()
    {
        // These methods need to be developed further.
        GenerateStreet();
        GenerateNumber();
        GenerateFloor();
        GenerateDoor();
        GetRandomPostalAndTown();
    }

    public string GenerateStreet(int length = 10)
    {
        StringBuilder streetName = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            char letter;

            // Decide whether to pick a lowercase or uppercase letter
            bool isUpperCase = _rand.Next(2) == 0;

            if (isUpperCase)
            {
                letter = (char)_rand.Next(65, 91); // ASCII values for A-Z
            }
            else
            {
                letter = (char)_rand.Next(97, 123); // ASCII values for a-z
            }

            streetName.Append(letter);
        }

       return Street = streetName.ToString();

    }

    public string GenerateNumber()
    {
        // Generate random number between 1 and 999 and optionally append an uppercase letter
        Number = new Random().Next(1, 1000).ToString();
        if (new Random().Next(0, 2) == 1) // 50% chance to add a letter
        {
            char letter = (char)new Random().Next(65, 91); // ASCII values for A-Z
             Number += letter;
        }

        return Number;
    }

    public string GenerateFloor()
    {
        int randomint = new Random().Next(0, 1);
        if (randomint == 0)
        {
           return Floor = "st";
        }
        else
        {
            randomint = new Random().Next(0, 99);
            return Floor = randomint.ToString();
        }
    }

    public string GenerateDoor()
    {
        string[] _prefixes = { "th", "mf", "tv" };
        int randomPreFix = _rand.Next(0, 3);
        int randomNumer = _rand.Next(0, 50);
        return Door = _prefixes[randomPreFix] + randomNumer.ToString();
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
                   return PostalCode = $"{reader.GetString("cPostalCode")} - {reader.GetString("cTownName")}";
                }
            }
        }
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"Street: {Street} {Number}");
        sb.AppendLine($"Floor: {Floor}");
        sb.AppendLine($"Door: {Door}");
        sb.AppendLine($"Postal Code and Town: {PostalCode}");

        return sb.ToString();
    }

}
