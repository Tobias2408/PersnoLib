using MySqlConnector;

namespace AddressTesting;

public class DbIntegrationTest
{
    [TestClass]
    public class PostalCodeTests
    {

        private MySqlConnection connection;
        private MySqlTransaction _transaction;

        private readonly string _connectionString = "Server=localhost;Port=3306;Database=addresses;User=user;Password=rootpassword;SSL Mode=None";

        private MySqlConnection _connection;
  

        [TestInitialize]
        public void Setup()
        {
            _connection = new MySqlConnection(_connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction(); // <-- Begin a new transaction
        }

        [TestCleanup]
        public void Teardown()
        {
            _transaction.Rollback();
            _connection.Close();
        }

        [TestMethod]
        public void TestTableCreation()
        {
            using (var command = new MySqlCommand("SHOW TABLES LIKE 'postal_code';", _connection))
            {
                command.Transaction = _transaction;
                var result = command.ExecuteScalar();
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void TestDataInsertion()
        {
            
            using (var command = new MySqlCommand("INSERT INTO `postal_code` (`cPostalCode`, `cTownName`) VALUES ('1799', 'København K');", _connection))
            {
                command.Transaction = _transaction;
                int affectedRows = command.ExecuteNonQuery();
                Assert.AreEqual(1, affectedRows);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(MySqlConnector.MySqlException))]
        public void TestRemoveDuplicates()
        {
            // Insert duplicate data for testing
            using (var insertCommand =
                   new MySqlCommand(
                       "INSERT INTO `postal_code` (`cPostalCode`, `cTownName`) VALUES ('1301', 'København K'), ('1301', 'København K');",
                       _connection))
            {
                insertCommand.Transaction = _transaction;
                insertCommand.ExecuteNonQuery();
            }
            
        }
        
        [TestMethod]
        public void TestRetrieveAllPostalCodes()
        {
            using (var command = new MySqlCommand("SELECT * FROM `postal_code`;", _connection))
            {
                command.Transaction = _transaction;
                using (var reader = command.ExecuteReader())
                {
                    int count = 0;
                    while (reader.Read())
                    {
                        count++;
                    }
                    Assert.IsTrue(count > 0, "Expected more than 0 postal codes but found none.");
                }
            }
        }

        [TestMethod]
        public void TestRetrieveSpecificPostalCode()
        {
            const string targetPostalCode = "1301";
            const string targetTownName = "København K";

            using (var command = new MySqlCommand($"SELECT * FROM `postal_code` WHERE `cPostalCode`='{targetPostalCode}';", _connection))
            {
                command.Transaction = _transaction;
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string retrievedPostalCode = reader.GetString("cPostalCode");
                        string retrievedTownName = reader.GetString("cTownName");

                        Assert.AreEqual(targetPostalCode, retrievedPostalCode);
                        Assert.AreEqual(targetTownName, retrievedTownName);
                    }
                    else
                    {
                        Assert.Fail($"Postal code {targetPostalCode} not found.");
                    }
                }
            }
        }

        [TestMethod]
        public void TestRetrieveNonExistentPostalCode()
        {
            const string nonExistentPostalCode = "9999";

            using (var command = new MySqlCommand($"SELECT * FROM `postal_code` WHERE `cPostalCode`='{nonExistentPostalCode}';", _connection))
            {
                command.Transaction = _transaction;
                using (var reader = command.ExecuteReader())
                {
                    if (!reader.Read())
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.Fail($"Unexpectedly found postal code {nonExistentPostalCode}.");
                    }
                }
            }
        }
        
    }
}