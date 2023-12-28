using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using BankDB.Account;

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
    }
}
