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
