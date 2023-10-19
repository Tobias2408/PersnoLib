using System.Text;
using Newtonsoft.Json;


namespace PersnoLib;

public class Person
{
    // Basic properties
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public string Cpr { get; set; }
    public DateTime DateOfBirth { get; set; } // Assuming using System;
    public Address Address { get; set; }
    public string MobilePhoneNumber { get; set; }
    
    
     public string[] ExtractNameFromJson(string filePath)
    {
        // Read the file and extract names and genders.
        // This is a basic example; you might need to adjust based on the .json file structure.
        var jsonData = File.ReadAllText(filePath);
        dynamic jsonObject = JsonConvert.DeserializeObject(jsonData);

        Name = jsonObject.Name;
        Surname = jsonObject.Surname;
        Gender = jsonObject.Gender;

        string[] data = new[] { Name, Surname, Gender };
        return data;

    }
    public string GenerateCPR()
    {
        // First six digits (date of birth in ddMMyy format)
        string dob = DateOfBirth.ToString("ddMMyy");

        // Last four digits, ensuring the constraint
        int lastDigit = Gender == "female" ? new Random().Next(0, 9999) * 2 % 10 : new Random().Next(0, 9999) * 2 + 1 % 10;
        string fourDigits = new Random().Next(0, 1000).ToString("D4"); // generates a random 4-digit number
        fourDigits = fourDigits.Substring(0, 3) + lastDigit.ToString();

        return Cpr = dob + fourDigits;
    }

    private readonly PhoneNumberValidator _validator = new PhoneNumberValidator();
    private readonly Random _random = new Random();
    public string GenerateMobilePhoneNumber()
    {
        // Vælg et tilfældigt præfiks fra validator's liste.
        string chosenPrefix = _validator.ValidPrefixes[_random.Next(_validator.ValidPrefixes.Length)];

        // Generer den resterende del af nummeret, så det i alt har præcis 8 cifre.
        StringBuilder remainingPart = new StringBuilder();
        for (int i = 0; i < 8; i++) // Fast antal iterationer for altid at få 8 cifre
        {
            remainingPart.Append(_random.Next(10)); // Tilføj et tilfældigt ciffer
        }

        // Kombiner det valgte præfiks med den genererede nummerdel.
        string fullNumber = chosenPrefix + remainingPart.ToString();

        return fullNumber; // Nu skulle dette være et gyldigt nummer ifølge din validator.
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"First Name: {Name}");
        sb.AppendLine($"Last Name: {Surname}");
        sb.AppendLine($"Gender: {Gender}");
        sb.AppendLine($"CPR: {Cpr}");
        sb.AppendLine($"Date of Birth: {DateOfBirth.ToString("yyyy-MM-dd")}");
        sb.AppendLine($"Address: {Address}");
        sb.AppendLine($"Mobile Phone Number: {MobilePhoneNumber}");

        return sb.ToString();
    }
}