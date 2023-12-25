using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankDB.Customer;

namespace UnitTest
{
    [TestClass]
    public class CustomerDALTests
    {
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
            customerEntity.SetEmail("TestEmail@gmail.com");
        }
    }
}
