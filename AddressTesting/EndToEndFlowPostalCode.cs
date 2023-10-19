using System.Text.RegularExpressions;
using PersnoLib;

namespace AddressTesting;

[TestClass]
public class EndToEndFlowPostalCode
{
    private MySqlPostalRepository _mySqlPostalRepository;
    private Address _address;

    [TestInitialize]
    public void TestInitialize()
    {
        _mySqlPostalRepository = new MySqlPostalRepository();
        _address = new Address();
    }

    [TestMethod]
    public void happyPath_PostalCodeGet()
    {
        _address.PostalCode = _mySqlPostalRepository.GetRandomPostalAndTown();
    
        // Assuming the output is in the format '1301, København K'
        string pattern = @"^(\d{4}) - ([A-ZÆØÅ][a-zæøå]+(?:[-\s][A-ZÆØÅa-zæøå]+)*)$";
        var regex = new Regex(pattern);

        Assert.IsTrue(regex.IsMatch(_address.PostalCode), "The postal code and town format does not match the expected pattern.");
    }
}