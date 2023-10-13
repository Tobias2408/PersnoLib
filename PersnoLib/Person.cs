using System.Text;
using Newtonsoft.Json;

namespace PersnoLib;

public class Person
{
    // Basic properties
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Gender { get; set; }
    public string CPR { get; set; }
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

        return CPR = dob + fourDigits;
    }

    public string GenerateMobilePhoneNumber()
    {
        // Given the constraints, this is a basic example. Expand as necessary.
        string[] validStarts = { "2", "30", "31", /*... other valid combinations ...*/ "826", "827", "829" };
        string chosenStart = validStarts[new Random().Next(validStarts.Length)];
        int remainingLength = 8 - chosenStart.Length;
        for (int i = 0; i < remainingLength; i++)
        {
            chosenStart += new Random().Next(10).ToString();
        }
       return MobilePhoneNumber = chosenStart;
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine($"First Name: {Name}");
        sb.AppendLine($"Last Name: {Surname}");
        sb.AppendLine($"Gender: {Gender}");
        sb.AppendLine($"CPR: {CPR}");
        sb.AppendLine($"Date of Birth: {DateOfBirth.ToString("yyyy-MM-dd")}");
        sb.AppendLine($"Address: {Address}");
        sb.AppendLine($"Mobile Phone Number: {MobilePhoneNumber}");

        return sb.ToString();
    }
}