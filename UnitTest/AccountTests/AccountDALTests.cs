using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using BankDB.Account;
using MySqlX.XDevAPI.Common;

namespace UnitTest
{
    [TestClass]
    public class AccountDALTests
    {
        internal string TestCanadaDBConnection = ConfigurationManager.AppSettings["TestCanadaDBConnection"];
        internal string TestBanKDBConnection = ConfigurationManager.AppSettings["TestBankDBConnection"];

        [TestMethod]
        public void TestCreateNewAccountValidCustomerID()
        {
            AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
            uint customerID = 1;

            try
            {
                bool isReturnTrue = accountDAL.CreateNewAccount(customerID);
                Assert.IsTrue(isReturnTrue);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void TestCreateNewAccountInvalidCustomerID()
        {
            AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
            uint customerID = 0;

            try
            {
                bool isReturnFalse = accountDAL.CreateNewAccount(customerID);
                Assert.IsFalse(isReturnFalse);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void TestGetCurrentBalanceValidAccountNumber()
        {
            AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
            uint accountNumber = 100000001;

            try
            {
                double currentBalance = accountDAL.GetCurrentBalance(accountNumber);
                Assert.IsTrue(currentBalance != double.NaN);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void TestGetCurrentBalanceInvalidAccountNumber()
        {
            AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
            uint accountNumber = 000000000;

            try
            {
                double currentBalance = accountDAL.GetCurrentBalance(accountNumber);
                Assert.IsTrue(double.IsNaN(currentBalance));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }
    }
}
