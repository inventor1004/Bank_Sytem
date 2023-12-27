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
        internal string TestBanKDBConnection = ConfigurationManager.AppSettings["TestBankDBConnection"];



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
                CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);
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
         * Function    : TestCreateNewAccountInvalidInput()
         * Desctription: This test is a test case to check whether CreateNewAccount() properly performs input validation.
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
                CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);
                int isReturnMinusOne = customerDAL.CreateNewAccount(customerEntity);
                Assert.AreEqual(-1, isReturnMinusOne);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

        [TestMethod]
        /*
         * Function    : TestCreateNewAccountDuplicateEmailAddress()
         * Desctription: This test is a test case to check whether CreateNewAccount() properly performs input validation.
         *               -* CreateNewAccount() should return -2 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestCreateNewAccountDuplicateEmailAddress()
        {
            // Does not set the Email property
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
                CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);
                int isReturnMinusOTwo = customerDAL.CreateNewAccount(customerEntity);
                Assert.AreEqual(-2, isReturnMinusOTwo);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestGetCustomerIDByEmailValidInput()
         * Desctription: This test case tests whether GetCustomerIDByEmail() retrieve the customerID properly
         *              based on the valid email input.
         *               -* CreateNewAccount() should return CustomerID(int) *-
         * Parameter   : void
         * Return      : void
         */
        public void TestGetCustomerIDByEmailValidInput()
        {
            CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);

            try
            {
                // Pass the valid email address which exists in Customer Table
                int customerID = customerDAL.GetCustomerIDByEmail("TestEmail@gmail.com");
                Assert.IsTrue(customerID > 0);
            }
            catch(Exception ex) 
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : TestGetCustomerIDByEmailInvalidInput()
         * Desctription: This test case tests whether GetCustomerIDByEmail() valids the input properly or not.
         *               -* CreateNewAccount() should return -2 *-
         * Parameter   : void
         * Return      : void
         */
        public void TestGetCustomerIDByEmailInvalidInput()
        {
            CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);

            try
            {
                // Pass the invalid email address which does not exist in Customer Table
                int customerID = customerDAL.GetCustomerIDByEmail("Invalid@gmail.com");
                Assert.IsTrue(customerID == -2);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }


        [TestMethod]
        /*
         * Function    : 
         * Desctription: 
         *               -* CreateNewAccount() should return List of CustomerEntity *-
         * Parameter   : void
         * Return      : void
         */
        public void TestGetCustomerTableByIdValidInput()
        {
            CustomerDAL customerDAL = new CustomerDAL(TestBanKDBConnection);

            try
            {
                // Pass the invalid email address which does not exist in Customer Table
                CustomerEntity customerEntity = customerDAL.GetCustomerTableById(1, TestCanadaDBConnection);
                Assert.IsTrue(customerEntity != null);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }
    }
}
