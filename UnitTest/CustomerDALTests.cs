using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankDB.Customer;
using System.Configuration;
using BankDB;

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
            CustomerEntity customerEntity = new CustomerEntity(TestCanadaDBConnection);
            DateTime testBirthDate = DateTime.Now;
            customerEntity.SetEmail("TestEmail@gmail.com");
            customerEntity.SetPassword("TestPassword123++");
            customerEntity.SetFirstName("Jhone");
            customerEntity.SetLastName("Smith");
            customerEntity.SetDateOfBirth(testBirthDate);
            customerEntity.SetPostalCode("A1A 1A1");
            customerEntity.SetProvince("Ontario");
            customerEntity.SetCity("Toronto");
            customerEntity.SetAddress("123ABC St N, Test 111");
            customerEntity.SetPhoneNumber("0123456789");

            try
            {
                // Pass the cutomer information to the CreateNewAccount() as a parameter
                CustomerDAL customerDAL = new CustomerDAL(TestBandDBConnection);
                int isReturnOne = customerDAL.CreateNewAccount(customerEntity);
                Assert.AreEqual(1, isReturnOne);
            }
            catch (Exception ex) 
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestCreateNewAccountValidInput()
         * Desctription: 
         *               
         *               -* CreateNewAccount() should return -1 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestCreateNewAccountInvalidInput()
        {
            // Does not set the Email property
            CustomerEntity customerEntity = new CustomerEntity(TestCanadaDBConnection);
            DateTime testBirthDate = DateTime.Now;
            customerEntity.SetPassword("TestPassword123++");
            customerEntity.SetFirstName("Jhone");
            customerEntity.SetLastName("Smith");
            customerEntity.SetDateOfBirth(testBirthDate);
            customerEntity.SetPostalCode("A1A 1A1");
            customerEntity.SetProvince("Ontario");
            customerEntity.SetCity("Toronto");
            customerEntity.SetAddress("123ABC St N, Test 111");
            customerEntity.SetPhoneNumber("0123456789");

            try
            {
                // Pass the cutomer information to the CreateNewAccount() as a parameter
                CustomerDAL customerDAL = new CustomerDAL(TestBandDBConnection);
                int isReturnMinusOne = customerDAL.CreateNewAccount(customerEntity);
                Assert.AreEqual(-1, isReturnMinusOne);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
