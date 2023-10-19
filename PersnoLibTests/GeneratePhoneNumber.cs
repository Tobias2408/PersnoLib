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
        public void IsValidPhoneNumber_LowestSingleDigitPrefix()
        {
            //ARRANGE Testing the phone number that starts with the lowest acceptable single digit prefix "2"
            var validNumberWithLowestPrefix = "212345678";

            // ACT  
            var result = _validator.IsValidPhoneNumber(validNumberWithLowestPrefix);

            // ASSERT
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsValidPhoneNumber_HighestMultiDigitPrefix()
        {
            // ARRANGE: Testing the phone number that starts with the highest acceptable multi-digit prefix "829".
            
            var validNumberWithHighestPrefix = "82912345678";

            // ACT: Test the phone number with the highest multi-digit prefix
            var result = _validator.IsValidPhoneNumber(validNumberWithHighestPrefix);

            // ASSERT: 
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsValidPhoneNumber_StartsWith31()
        {
            // ARRANGE: A valid phone number that starts with "31"
            var validNumber = "3123456789";

            // ACT: Test the phone number
            var result = _validator.IsValidPhoneNumber(validNumber);

            // ASSERT: Check the result
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_StartsWith476()
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

        //Might remove code bellow - test does not make sense/have value, since we already tested invalid prefix
        //Might remove code bellow - test does not make sense/have value, since we already tested invalid prefix
        //Might remove code bellow - test does not make sense/have value, since we already tested invalid prefix
        [TestMethod]
        public void IsValidPhoneNumber_JustBelowLowestSingleDigitPrefix()
        {
            // ARRANGE: Testing the phone number that starts with a prefix just below the lowest acceptable single-digit prefix, "1" in this case.
            var numberWithBelowLowestPrefix = "1123456789";

            // ACT: Test the phone number with the prefix that is just below the acceptable range
            var result = _validator.IsValidPhoneNumber(numberWithBelowLowestPrefix);

            // ASSERT: Check the result, expecting a failure as "1" is an invalid prefix.
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_JustAboveLowestSingleDigitPrefix()
        {
            // ARRANGE: Testing the phone number that starts with a prefix just above the lowest acceptable single-digit prefix, "3" in this case.
            var numberWithAboveLowestPrefix = "3223456789";

            // ACT: Test the phone number with the prefix that is just above the acceptable range
            var result = _validator.IsValidPhoneNumber(numberWithAboveLowestPrefix);

            // ASSERT: Check the result, expecting a failure as "3" is a invalid prefix. Unless its 30 or 31. Which is why we used 32.
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_JustBelowHighestMultiDigitPrefix()
        {
            // ARRANGE: Testing the phone number that starts with a prefix just below the highest acceptable multi-digit prefix, "828" in this case.
            var numberWithBelowHighestPrefix = "8281234567";

            // ACT: Test the phone number with the prefix that is just below the highest acceptable range
            var result = _validator.IsValidPhoneNumber(numberWithBelowHighestPrefix);

            // ASSERT: Check the result, expecting a failure as "828" is an invalid prefix.
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidPhoneNumber_JustAboveHighestMultiDigitPrefix()
        {
            // ARRANGE: Testing the phone number that starts with a prefix just above the highest acceptable multi-digit prefix, "830" in this case.
            var numberWithAboveHighestPrefix = "8301234567";

            // ACT: Test the phone number with the prefix that is just above the highest acceptable range
            var result = _validator.IsValidPhoneNumber(numberWithAboveHighestPrefix);

            // ASSERT: Check the result, expecting a failure as "830" is outside the range of valid prefixes.
            Assert.IsFalse(result);
        }

    }
}
