using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PersnoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersnoLib.Tests
{
    [TestClass()]
    public class NameAndGenderTests
    {
        private const string FILE_NAMES_PATH =
            "C:\\Users\\slm31\\source\\repos\\PersnoLib\\persons-names.json";
        private Person _person;

        [TestInitialize]
        public void Setup()
        {
            _person = new Person();
        }

        [TestMethod()]
        public void ExtractNameFromJson_IsNotNullTest()
        {
            // Act
            _person.ExtractNameAndGenderFromJson(FILE_NAMES_PATH);

            // Assert
            Assert.IsNotNull(_person);

        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ExtractNameFromJson_ReadFromFile_IncorrectPath_ThrowsFileNotFoundException()
        {
            // Arrange
            var INCORRECT_FILE_PATH = "nonexistent.json";

            // Act
            // Try reading from a file that doesn't exist
            _person.ExtractNameAndGenderFromJson(INCORRECT_FILE_PATH);
        }

        [TestMethod]
        public void ExtractNameFromJson_InvalidJson_ThrowsException()
        {
            // Setup an invalid JSON content in a temporary file
            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "invalid_json_content_here");

            // Create an instance of our class containing the ExtractNameFromJson method
            var personInstance = new Person();

            try
            {
                personInstance.ExtractNameAndGenderFromJson(tempFile);
                Assert.Fail("Expected a JSON parsing exception but none was thrown.");
            }
            catch (JsonReaderException)  // This is thrown for invalid JSON structure
            {
                // Expected behavior
            }
            finally
            {
                File.Delete(tempFile);
            }
        }

        [TestMethod]
        public void ExtractNameFromJson_EmptyPersonsArray()
        {
            // Act
            var result = File.ReadAllText(FILE_NAMES_PATH);

            var names = JObject.Parse(result);
            var persons = names["persons"].ToObject<JArray>();

            // Assert
            // Checks if the persons array is empty
            Assert.IsFalse(persons.Count == 0, "The 'persons' array is empty.");

        }

        [TestMethod()]
        [DataRow("Annemette P.", "Nilsson", "female")]
        [DataRow("Freja O.", "Thygesen", "female")]
        [DataRow("Nicolai T.", "Bech", "male")]
        [DataRow("Martin E.", "Lauridsen", "male")]
        public void ExtractNameFromJson_ExistInFileTest(string expectedName, string expectedSurname, string expectedGender)
        {
            // Arrange
            string jsonFilePath = "C:\\Users\\slm31\\source\\repos\\PersnoLib\\persons-names.json";

            // Act
            var result = File.ReadAllText(jsonFilePath);

            var names = JObject.Parse(result);
            var persons = names["persons"].ToObject<JArray>();
            var existsInFile = persons.Any(p =>
                p["name"].Value<string>() == expectedName &&
                p["surname"].Value<string>() == expectedSurname &&
                p["gender"].Value<string>() == expectedGender
                );

            // Assert
            Assert.IsTrue(existsInFile, "The person does not exist in the file");
        }

        [TestMethod]
        [DataRow("name")]
        [DataRow("surname")]
        [DataRow("gender")]
        public void ExtractNameFromJson_PropertiesAreNotNullOrEmptyTest(string attributeToCheck)
        {
            // Act
            var result = File.ReadAllText(FILE_NAMES_PATH);
            var names = JObject.Parse(result);
            var persons = names["persons"].ToObject<JArray>();

            bool allAttributesValid = persons.All(p =>
                !string.IsNullOrEmpty(p[attributeToCheck]?.Value<string>())
            );

            // Assert
            Assert.IsTrue(allAttributesValid, $"Some {attributeToCheck} values are null or empty in the file");
        }

        [TestMethod]
        public void PropertiesAreNotEmpty()
        {
            // Act
            _person.ExtractNameAndGenderFromJson(FILE_NAMES_PATH);

            // Assert
            Assert.IsFalse(string.IsNullOrEmpty(_person.Name), "FirstName is null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(_person.Surname), "LastName is null or empty.");
            Assert.IsFalse(string.IsNullOrEmpty(_person.Gender), "Gender is null or empty.");
        }

        [TestMethod]
        public void ExtractNameFromJson_GenderValuesAreValidTest()
        {
            // Act
            var result = File.ReadAllText(FILE_NAMES_PATH);
            var names = JObject.Parse(result);
            var persons = names["persons"].ToObject<JArray>();

            bool allGendersValid = persons.All(p =>
            p["gender"].Value<string>() == "male" || p["gender"].Value<string>() == "female"
            );

            // Assert
            Assert.IsTrue(allGendersValid, "Some gender values in the file are not 'male' or 'female'");
        }
    }
}