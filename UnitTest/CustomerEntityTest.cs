using System;
using BankDB;
using System.Configuration;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankDB.Customer;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace UnitTest
{
    [TestClass]
    public class CustomerEntityTest
    {
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
                int isreturnMinusTwo = entity.SetEmail(validEmail);
                Assert.IsTrue(isreturnMinusTwo == 1);

                isreturnMinusTwo = entity.SetEmail(validEmailWithSpecialChar);
                Assert.IsTrue(isreturnMinusTwo == 1);
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
                int isreturnMinusTwo = entity.SetEmail(invalidLocalPart);
                Assert.IsTrue(isreturnMinusTwo == -2);

                // Check whether it is validated well if the domain part including @ is missing in the email
                isreturnMinusTwo = entity.SetEmail(missingDomainPart);
                Assert.IsTrue(isreturnMinusTwo == -2);

                // Check whether it is validated well if the top-level domain part including . is missing in the email
                isreturnMinusTwo = entity.SetEmail(missingTopLevelDomainPart);
                Assert.IsTrue(isreturnMinusTwo == -2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
