using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankDB.Customer;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class CustomerDALTests
    {
        internal string TestCanadaDBConnection = ConfigurationManager.AppSettings["TestCanadaDBConnection"];
        internal string TestBandDBConnection = ConfigurationManager.AppSettings["TestBankDBConnection"];
        [TestMethod]
        /*
         * Function    : TestCreateNewAccountValidInput()
         * Desctription: Test whether CreateNewAccount() method add new colon in the customer table in MySQL database or not
         *               >> The valid CustomerEntity instance that all properties are filled appropriately will be passed as an input
         *               -* CreateNewAccount() should return 1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestCreateNewAccountValidInput()
        {
            CustomerEntity customerEntity = new CustomerEntity();
            DateTime testBirthDate = DateTime.Now;
            customerEntity.SetEmail("TestEmail@gmail.com");
            customerEntity.SetPassword("TestPassword123++");
            customerEntity.SetFirstName("Jhone");
            customerEntity.SetLastName("Smith");
            customerEntity.SetDateOfBirth(testBirthDate);
            customerEntity.SetPostalCode("A1A 1A1");
            customerEntity.SetProvince("Ontario", TestCanadaDBConnection);


        }
    }
}
