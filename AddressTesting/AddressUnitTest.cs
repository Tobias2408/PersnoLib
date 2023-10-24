using PersnoLib;

namespace AddressTesting;

[TestClass]
public class AddressUnitTest
{
    private Address? _addressGenerator;
    private PostalService? _postalService;

    [TestInitialize]
    public void TestInitialize()
    {
        _addressGenerator = new Address();
        _postalService = new PostalService();
    }
    
    // Street Name
    [TestMethod]
    public void TestGenerateStreet_DefualtLength()
    {
        var result = _addressGenerator.GenerateStreet();
        
        Assert.AreEqual(10, result.Length, "The default length of the generated street name should be 10.");
    }

    [TestMethod]
    public void TestGenerateStreet_AllIsCharacters()
    {
        var result = _addressGenerator.GenerateStreet();

        Assert.IsTrue(result.All(char.IsLetter), "The generated street name should contain only letters.");
    }
    
    [TestMethod]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(250)]
    [DataRow(500)]
    [DataRow(501)]
   //insufficient memory [DataRow(Int32.MaxValue)]
    // insufficient memory[DataRow(Int32.MaxValue-1)]
    public void TestGenerateStreet_VaryingLengths_Correct(int length)
    {
        var result = _addressGenerator.GenerateStreet(length);

        Assert.AreEqual(length, result.Length, $"The length of the generated street name should be {length}.");
        Assert.IsTrue(result.All(char.IsLetter), "The generated street name should contain only letters.");
    }
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
   // [DataRow(Int32.MaxValue+1)] invalid input
    public void TestGenerateStreet_Lengths0_Error()
    {
        var result = _addressGenerator.GenerateStreet(0);
    }
   
   // Number 
   
   [TestMethod]
   [DataRow(0)]
   [DataRow(1)]
   [DataRow(2)]
   [DataRow(998)]
   [DataRow(999)]
   [DataRow(1000)]
   [DataRow(500)]
   public void GenerateNumber_ShouldBeWithinExpectedNumericRange(int expectedNumeric)
   {
       var result = _addressGenerator.GenerateNumber();
       var numericPart = int.Parse(new string(result.TakeWhile(char.IsDigit).ToArray()));
       Assert.IsTrue(numericPart >= 1 && numericPart <= 999, $"Unexpected numeric value: {numericPart}.");
   }
   [TestMethod]
   [DataRow(-1)]
   [DataRow(Int16.MinValue)]
   [DataRow(Int16.MinValue+1)]
   [DataRow(Int16.MinValue-1)]
   public void GenerateNumber_NotMinusWithinExpectedNumericRange(int expectedNumeric)
   {
       var result = _addressGenerator.GenerateNumber();
       var numericPart = int.Parse(new string(result.TakeWhile(char.IsDigit).ToArray()));
       Assert.IsTrue(numericPart >= 1 && numericPart <= 999, $"Unexpected numeric value: {numericPart}.");
   }
   
   [TestMethod]
   [DataRow(Int16.MaxValue)]
   [DataRow(Int16.MaxValue+1)]
   [DataRow(Int16.MaxValue-1)]
   public void GenerateNumber_NotPlusWithinExpectedNumericRange(int expectedNumeric)
   {
       var result = _addressGenerator.GenerateNumber();
       var numericPart = int.Parse(new string(result.TakeWhile(char.IsDigit).ToArray()));
       Assert.IsTrue(numericPart >= 1 && numericPart <= 999, $"Unexpected numeric value: {numericPart}.");
   }
   [TestMethod]
       [DataRow('A')]
       [DataRow('Z')]
       [DataRow('M')]
       public void GenerateNumber_ShouldContainAtMostOneUppercaseLetter(char expectedLetter)
       {
           var result = _addressGenerator.GenerateNumber();
           var letters = result.Where(char.IsLetter).ToList();
        
           Assert.IsTrue(letters.Count <= 1, $"Expected 0 or 1 letters, but got {letters.Count}.");
           if (letters.Count == 1) Assert.IsTrue(char.IsUpper(letters.First()), $"Expected uppercase letter but got: {letters.First()}.");
       }
       
    [TestMethod]
    [DataRow(2)]
    public void GenerateNumber_ShouldNotContainInvalidNumberOfLetters(int invalidLetterCount)
    {
        var result = _addressGenerator.GenerateNumber();
        var letterCount = result.Count(char.IsLetter);

        Assert.AreNotEqual(invalidLetterCount, letterCount, $"Unexpected number of letters: {letterCount}.");
    }

   [TestMethod]
   public void GenerateNumber_ShouldNotContainSpecialCharacters()
   {
       var result = _addressGenerator.GenerateNumber();
       Assert.IsFalse(result.Any(c => !char.IsLetterOrDigit(c)), $"Unexpected special character in: {result}.");
   }

   // Floor testing 
   
   [TestMethod]
   [DataRow(0)]
   [DataRow(1)]
   [DataRow(2)]
   [DataRow(98)]
   [DataRow(99)]
   [DataRow(100)]
   [DataRow(50)]
   public void GenerateFloor_NumericBoundaries_Valid(int expectedValue)
   {
       var result = _addressGenerator.GenerateFloor();
       bool isNumeric = int.TryParse(result, out int numericValue);
       if(isNumeric) 
       {
           Assert.IsTrue(numericValue >= 1 && numericValue <= 99);
       }
   }
   
   [TestMethod]
   [DataRow(-1)]
   [DataRow(0)]
   [DataRow(1)]
   [DataRow(-50)]
   public void GenerateFloor_NumericBelowValidRange_Invalid(int expectedValue)
   {
       var result = _addressGenerator.GenerateFloor();
       bool isNumeric = int.TryParse(result, out int numericValue);
       if(isNumeric) 
       {
           Assert.IsFalse(numericValue < 1);
       }
   }
   
   [TestMethod]
   [DataRow(99)]
   [DataRow(100)]
   [DataRow(101)]
   public void GenerateFloor_NumericAboveValidRange_Invalid(int expectedValue)
   {
       var result = _addressGenerator.GenerateFloor();
       bool isNumeric = int.TryParse(result, out int numericValue);
       if(isNumeric) 
       {
           Assert.IsFalse(numericValue > 99);
       }
   }
   [TestMethod]
   [DataRow(-1)]
   [DataRow(0)]
   [DataRow(1)]
   public void GenerateFloor_NumericZero_Invalid(int expectedValue)
   {
       var result = _addressGenerator.GenerateFloor();
       bool isNumeric = int.TryParse(result, out int numericValue);
       if(isNumeric) 
       {
           Assert.AreNotEqual(0, numericValue);
       }
   }
   
   [TestMethod]
   public void GenerateFloor_ST_Valid()
   {
       var result = _addressGenerator.GenerateFloor();
       if(result == "st") 
       {
           Assert.AreEqual("st", result);
       }
   }
   [TestMethod]
   public void GenerateFloor_NoSpecialCharacters()
   {
       var result = _addressGenerator.GenerateFloor();
       Assert.IsFalse(result.Any(ch => !char.IsLetterOrDigit(ch)));
   }

   [TestMethod]
   public void GenerateFloor_NotWords()
   {
       var result = _addressGenerator.GenerateFloor();
       if(result.Length > 2)
       {
           Assert.IsFalse(int.TryParse(result, out _)); // Checking if result is not numeric
       }
   }
   
   [TestMethod]
   public void GenerateFloor_NotLowerCaseSt()
   {
       var result = _addressGenerator.GenerateFloor();
       Assert.AreNotEqual("st", result);
   }

    // Doors 
    
    [TestMethod]
    [DataRow(0)]
    [DataRow(1)]
    [DataRow(2)]
    [DataRow(49)]
    [DataRow(50)]
    [DataRow(51)]
    [DataRow(25)]
    public void GenerateDoor_NumericBoundaries_1_To_50(int expectedValue)
    {
        var result = _addressGenerator.GenerateDoor();
        int numericPart = int.Parse(result.Substring(2)); // Extracts numeric part
        Assert.IsTrue(numericPart >= 0 && numericPart <= 49);
    }
    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(-200)]
    public void GenerateDoor_NumericBelowValidRange_Invalid(int expectedValue)
    {
        var result = _addressGenerator.GenerateDoor();
        int numericPart = int.Parse(result.Substring(2)); // Extracts numeric part
        Assert.IsFalse(numericPart < 0);
    }

    [TestMethod]
    [DataRow(50)]
    [DataRow(51)]
    [DataRow(52)]
    [DataRow(300)]
    public void GenerateDoor_NumericAboveValidRange_50(int expectedValue)
    {
        var result = _addressGenerator.GenerateDoor();
        int numericPart = int.Parse(result.Substring(2)); // Extracts numeric part
        Assert.IsFalse(numericPart > 49);
    }
    
    //postal unit test With mock 
    [TestMethod]
    public void GetRandomPostalAndTown_ShouldReturnValidFormat()
    {
        // Act
        var result = _postalService.GetRandomPostalAndTown();

        // Assert
        Assert.IsNotNull(result);
        Assert.IsFalse(string.IsNullOrEmpty(result));

        // Splitting the result to verify postal and town.
        var parts = result.Split(" - ");
        Assert.AreEqual(2, parts.Length);

        // Ensure the postal code and town are from the predefined lists.
        Assert.IsTrue(_postalService.PostalCodes.Contains(parts[0]));
        Assert.IsTrue(_postalService.Towns.Contains(parts[1]));
    }
}