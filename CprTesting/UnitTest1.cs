namespace CprTesting
{
    using PersnoLib;
    [TestClass]
    public class UnitTest1
    {
        private Person personMale;
        private Person personFemale;
        [TestInitialize]
        public void testInitialize()
        {
            personFemale = new Person();
            personMale = new Person();

            personFemale.DateOfBirth = DateTime.Now;
            personFemale.Gender = "female";


            personMale.DateOfBirth = DateTime.Now;
            personMale.Gender = "male";

        }

        [TestMethod]
        public void Test_CPR_Length_Is_9_False()
        {

            // Act
            string cpr = personFemale.GenerateCPR();

            // Assert that the CPR number length is not 9
            Assert.IsFalse(cpr.Length == 9, "CPR number has a length of 9 (should be False).");
        }

        [TestMethod]
        public void Test_CPR_Length_Is_10_True()
        {

            // Act
            string cpr = personFemale.GenerateCPR();
            Console.WriteLine(cpr);

            // Assert that the CPR number length is 10
            Assert.IsTrue(cpr.Length == 10, "CPR number does have a length of 10 (should be True).");
        }



        [TestMethod]
        public void Test_CPR_Length_Is_11_False()
        {
            // Arrange


            // Act
            string cpr = personFemale.GenerateCPR();

            // Assert that the CPR number length is not 11
            Assert.IsFalse(cpr.Length == 11, "CPR number has a length of 11 (should be False).");
        }


        [TestMethod]
        public void Test_CPR_First6DigitsMatchDOB()
        {

            // Act
            string cpr = personFemale.GenerateCPR();
            string dobPart = cpr.Substring(0, 6);

            // Assert
            string expectedDOB = personFemale.DateOfBirth.ToString("ddMMyy");
            Assert.AreEqual(expectedDOB, dobPart);
        }




        [TestMethod]
        public void Test_Last_Digit_Is_Odd_For_Male_True()
        {

            // Act
            string cpr = personMale.GenerateCPR();
            int lastDigit = int.Parse(cpr.Substring(cpr.Length - 1, 1));

            // Assert that the last digit is odd for males
            Assert.IsTrue(lastDigit % 2 == 1, "Last digit of CPR for male is not odd (should be True).");
        }

        [TestMethod]
        public void Test_Last_Digit_Is_Odd_For_Male_False()
        {


            // Act
            string cpr = personMale.GenerateCPR();
            int lastDigit = int.Parse(cpr.Substring(cpr.Length - 1, 1));

            // Assert that the last digit is not even for males
            Assert.IsFalse(lastDigit % 2 == 0, "Last digit of CPR for male is even (should be False).");
        }

        [TestMethod]
        public void Test_Last_Digit_Is_Even_For_Female_True()
        {


            // Act
            string cpr = personFemale.GenerateCPR();
            int lastDigit = int.Parse(cpr.Substring(cpr.Length - 1, 1));


            // Assert that the last digit is even for females
            Assert.IsTrue(lastDigit % 2 == 0, "Last digit of CPR for female is not even (should be True).");
        }

        [TestMethod]
        public void Test_Last_Digit_Is_Even_For_Female_False()
        {


            // Act
            string cpr = personFemale.GenerateCPR();
            int lastDigit = int.Parse(cpr.Substring(cpr.Length - 1, 1));

            // Assert that the last digit is not odd for females
            Assert.IsFalse(lastDigit % 2 == 1, "Last digit of CPR for female is odd (should be False).");
        }

        [TestMethod]
        public void Test_CPR_Below10()
        {


            // Act
            string cpr = personFemale.GenerateCPR();

            // Assert
            int lastDigit = int.Parse(cpr.Substring(8, 1));
            Assert.IsTrue(lastDigit < 10);
        }



    }
}