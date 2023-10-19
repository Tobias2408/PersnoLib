using Microsoft.VisualStudio.TestTools.UnitTesting;
using PersnoLib;

namespace PhoneNumberTesting
{
    [TestClass]
    public class PhoneNumberValidatorTests
    {
        private PhoneNumberValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            // ARRANGE: Here we start by creating a new validator
            _validator = new PhoneNumberValidator();
        }

        [TestMethod]
        public void IsValidPhoneNumber_ValidNumber_StartsWith31()
        {
            // ARRANGE: A valid phone number that starts with "31"
            var validNumber = "3123456789";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(validNumber);

            // ASSERT: Check the result
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_ValidNumber_StartsWith476()
        {
            // ARRANGE: A valid phone number that starts with "467"
            var validNumber = "47612345678";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(validNumber);

            // ASSERT: Check the result
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_InvalidPrefix()
        {
            // ARRANGE: A phone number with an invalid prefix
            var invalidNumber = "9912345678";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(invalidNumber);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_TooShort()
        {
            // ARRANGE: A phone number that is too short
            var shortNumber = "311234567";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(shortNumber);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_TooLong()
        {
            // ARRANGE: A phone number that is too long
            var longNumber = "3123456789012";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(longNumber);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_WithSpecialCharacters()
        {
            // ARRANGE: Number with special characters
            var numberWithSpecialChars = "31#245^789";

            // ACT: Test number with special characters
            var result = _validator.IsValidPhoneNumber(numberWithSpecialChars);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }


        [TestMethod]
        public void IsValidPhoneNumber_WithBrackets()
        {
            // ARRANGE: Number with brackets
            var numberWithBrackets = "(31)2345678";

            // ACT: Test number with brackets
            var result = _validator.IsValidPhoneNumber(numberWithBrackets);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_WithSpaces()
        {
            // ARRANGE: Number with spaces within
            var numberWithSpaces = "31 2345 6789";

            // ACT: Test number with spaces
            var result = _validator.IsValidPhoneNumber(numberWithSpaces);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_WithDashes()
        {
            // ARRANGE: Number with dashes within
            var numberWithDashes = "31-234-56789";

            // ACT: Test number with dashes
            var result = _validator.IsValidPhoneNumber(numberWithDashes);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_Alphanumeric()
        {
            // ARRANGE: Alphanumeric phone number (both numbers and letters together)
            var alphaNumeric = "31234ABCD9";

            // ACT: Test alphanumeric number
            var result = _validator.IsValidPhoneNumber(alphaNumeric);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_WithUnicodeCharacters()
        {
            // ARRANGE: Number with unicode characters
            var numberWithUnicode = "31\u266B234567";

            // ACT: Test number with unicode
            var result = _validator.IsValidPhoneNumber(numberWithUnicode);

            // ASSERT: Check the result
            Assert.IsFalse(result);
        }
    }
}
