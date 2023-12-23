using System;
using BankDB;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDB.Customer;

namespace UnitTest
{
    [TestClass]
    public class CustomerEntityTests
    {
        private string connectionString = ConfigurationManager.AppSettings["connectionString"];

        [TestMethod]
        /*
         * Function    : TestSetEmailValidPattern()
         * Desctription: Test whether the SET method sets the email address properly or not
         *               >> 1. Valid email address 
         *               >> 2. Valid email address with allowable special characters
         *               -* the SetEmail() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetEmailValidPattern()
        {
            CustomerEntity entity = new CustomerEntity();
            string validEmail = "validPatern@gmail.com";                        // Valid email address 
            string validEmailWithSpecialChar = "validPatern._%+-@gmail.com";    // Valid email address with allowable special characters

            try
            {
                // Test whether the email address sets successfully
                int isReturnOne = entity.SetEmail(validEmail);
                Assert.IsTrue(isReturnOne == 1);

                isReturnOne = entity.SetEmail(validEmailWithSpecialChar);
                Assert.IsTrue(isReturnOne == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
       

        [TestMethod]
        /*
         * Function    : TestSetEmailInvalidPattern()
         * Desctription: Test whether the SET method of Email property catches invalid inputs or not
         *               >> 1. Invalid email input which doesn't follow the email pattern
         *               >> 2. Invalid email input which doesn't have email domain part (after '@' ~ before '.')
         *               >> 3. Invalid email input which doesn't have email top-level domain part (after '.')
         *               -* the SetEmail() should return -2 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetEmailInvalidPattern()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidLocalPart = "Invalid()Patern@gmail.com";
            string missingDomainPart = "InvalidEmailAddressPattern";
            string missingTopLevelDomainPart = "InvalidPatern@gmail";

            try
            {
                // Check if validation is successful when entering special characters not included in the pattern
                int isReturnMinusTwo = entity.SetEmail(invalidLocalPart);
                Assert.IsTrue(isReturnMinusTwo == -2);

                // Check whether it is validated well if the domain part including @ is missing in the email
                isReturnMinusTwo = entity.SetEmail(missingDomainPart);
                Assert.IsTrue(isReturnMinusTwo == -2);

                // Check whether it is validated well if the top-level domain part including . is missing in the email
                isReturnMinusTwo = entity.SetEmail(missingTopLevelDomainPart);
                Assert.IsTrue(isReturnMinusTwo == -2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetPasswordValidPattern()
         * Desctription: Test whether the SETPassword method sets the password properly or not
         *               >> Validation Condition: 1. Uppercase letter
         *                                        2. Lowercase letter
         *                                        3. Number
         *                                        4. Special character
         *                                        5. Over 10 characters
         *              -* the SetEmail() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetPasswordValidPattern()
        {
            CustomerEntity entity = new CustomerEntity();
            string validPassword = "Ab1!@#$%^&*()-_+=";

            try
            {
                // Test whether the passowrd sets successfully
                int isReturnOne = entity.SetPassword(validPassword);
                Assert.IsTrue(isReturnOne == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetPasswordInvalidShorterLength()
         * Desctription: Test whether the SETPassword method catch invalid length of the password input properly or not
         *                >> The test input has all the essential components, but the length is a total of 4 characters, 
         *                  which is less than the minimum requirement of 10 characters.
         *              -* the SetEmail() should return -1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetPasswordInvalidShorterLength()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidPassword = "Ab1!";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusOne = entity.SetPassword(invalidPassword);
                Assert.IsTrue(isReturnMinusOne == -1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestSetPasswordInvalidLongerLength()
         * Desctription: Test whether the SETPassword method catch invalid length of the password input properly or not
         *                >> The test input has all the essential components, but the length is a total of 30 characters, 
         *                  which exceeds than the maximum requirement of 20 characters.
         *              -* the SetEmail() should return -2 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetPasswordInvalidLongerLength()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidPassword = "ABCDEFabcdef123456!@#$%^&*()-_";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusTwo = entity.SetPassword(invalidPassword);
                Assert.IsTrue(isReturnMinusTwo == -2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetPasswordInvalidMissingCharacter()
         * Desctription: Test whether the SETPassword method catches invalid pattern of the password input properly or not
         *               It will be tested by below input conditions:
         *                >> 1. Missing lowercase letter
         *                >> 2. Missing uppercase letter
         *                >> 3. Missing number
         *                >> 4. Missing speical chracter
         *              -* the SetEmail() should return -3 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetPasswordInvalidMissingCharacter()
        {
            CustomerEntity entity = new CustomerEntity();
            string specialCharacterMissed = "ABCDabcd1234";
            string numberMissed           = "ABCDabcd!@#$";
            string lowerCaseMissed        = "ABCD1234!@#$";
            string upperCaseMissed        = "abcd1234!@#$";


            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusThree = entity.SetPassword(upperCaseMissed);
                Assert.IsTrue(isReturnMinusThree == -3);

                isReturnMinusThree = entity.SetPassword(lowerCaseMissed);
                Assert.IsTrue(isReturnMinusThree == -3);

                isReturnMinusThree = entity.SetPassword(numberMissed);
                Assert.IsTrue(isReturnMinusThree == -3);

                isReturnMinusThree = entity.SetPassword(specialCharacterMissed);
                Assert.IsTrue(isReturnMinusThree == -3);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestSetFirstNameValidInput()
         * Desctription: Test whether the SetFirstName sets the value properly when a valid input is passed.
         *              -* the SetFristName() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetFirstNameValidInput()
        {
            CustomerEntity entity = new CustomerEntity();
            string validFirstName = "Test";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnOne = entity.SetFirstName(validFirstName);
                Assert.IsTrue(isReturnOne == 1);            
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetFirstNameInValidInput()
         * Desctription: Test whether the SetFirstName() validate properly when an invalid input is passed
         *              -* the SetFristName() should return -1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetFirstNameInValidInput()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidFirstName = "012345678901234567890123456789012345678901234567890";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusOne = entity.SetFirstName(invalidFirstName);
                Assert.IsTrue(isReturnMinusOne == -1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestSetLastNameValidInput()
         * Desctription: Test whether the SetLastName sets the value properly when a valid input is passed.
         *              -* the SetLastName() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetLastNameValidInput()
        {
            CustomerEntity entity = new CustomerEntity();
            string validLastName = "Test";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnOne = entity.SetLastName(validLastName);
                Assert.IsTrue(isReturnOne == 1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestSetFirstNameInValidInput()
         * Desctription: Test whether the SetLastName() validate properly when an invalid length of input is passed
         *              -* the SetFristName() should return -1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetLastNameInValidLength()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidLastName = "012345678901234567890123456789012345678901234567890";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusOne = entity.SetLastName(invalidLastName);
                Assert.IsTrue(isReturnMinusOne == -1);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetFirstNameInValidInput()
         * Desctription: Test whether the SetLastName() validate properly when an invalid pattern of input(contains blank) is passed
         *              -* the SetFristName() should return -2 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetLastNameInValidPattern()
        {
            CustomerEntity entity = new CustomerEntity();
            string invalidLastName = "Last Name";

            try
            {
                // Test whether the SET method identify invalid length and return proper value for the invalid status
                int isReturnMinusOne = entity.SetLastName(invalidLastName);
                Assert.IsTrue(isReturnMinusOne == -2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestSetLPostalCoedValidInput()
         * Desctription: Check SetPostalCode() sets the value properly when entering the postal code of the correct pattern
         *              -* the SetFristName() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetLPostalCoedValidInputs()
        {
            CustomerEntity entity = new CustomerEntity();
            string postalCodeWithoutSpace = "A1A1A1";       // Postal Code without space 
            string postalLowerCaseLetter= "a1a1a1";         // Postal Code with lowercase letters
            string postalCodeWithSpace = "A1A 1A1";         // Postal Code with space 


            try
            {
                // Test whether the SET method sets the postal code properly 
                int isReturnOne = entity.SetPostalCode(postalCodeWithoutSpace);
                Assert.IsTrue(isReturnOne == 1);

                isReturnOne = entity.SetPostalCode(postalLowerCaseLetter);
                Assert.IsTrue(isReturnOne == 1);

                isReturnOne = entity.SetPostalCode(postalCodeWithSpace);
                Assert.IsTrue(isReturnOne == 1);


            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetLPostalCoedInvalidInputs()
         * Desctription: Check SetPostalCode() catches an invalid postal code inputs
         *              -* the SetFristName() should return -1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetLPostalCoedInvalidInputs()
        {
            CustomerEntity entity = new CustomerEntity();
            string lengthIncorrect = "A1A1A1A";    
            string extraSpace      = "a1a 1a1 ";    
            string reversedPattern = "1A1 A1A";       


            try
            {
                // Test whether the SET method sets the postal code properly 
                int isReturnMinusOne = entity.SetPostalCode(lengthIncorrect);
                Assert.IsTrue(isReturnMinusOne == -1);

                isReturnMinusOne = entity.SetPostalCode(extraSpace);
                Assert.IsTrue(isReturnMinusOne == -1);

                isReturnMinusOne = entity.SetPostalCode(reversedPattern);
                Assert.IsTrue(isReturnMinusOne == -1);


            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetDateOfBirthValidInput()
         * Desctription: Check SetDateOfBirth() sets the customer's date of birth properly if it is a valid input
         *              -* the SetFristName() should return true *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetDateOfBirthValidInput()
        {
            CustomerEntity entity = new CustomerEntity();
            DateTime validDateOfBirth = new DateTime(2023, 12, 22);

            try
            {
                // Test whether the SET method sets the customer's date of birth properly 
                bool isReturnTrue = entity.SetDateOfBirth(validDateOfBirth);
                Assert.IsTrue(isReturnTrue);

            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestSetDateOfBirthInvalidInput()
         * Desctription: Check SetDateOfBirth() catches invalid input (future date) properly or not
         *              -* the SetFristName() should return false *-
         * Parameter   : void
         * Return      : void
         */
        public void TestSetDateOfBirthInvalidInput()
        {
            CustomerEntity entity = new CustomerEntity();
            DateTime currentDate = DateTime.Now;            // The current date
            DateTime futureDate = currentDate.AddYears(1);  // The date one year after

            try
            {
                // Test whether the SET method catches the error when it takes invalid date as an input 
                bool isReturnFalse = entity.SetDateOfBirth(futureDate);
                Assert.IsFalse(isReturnFalse);

            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
