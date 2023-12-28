using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using BankDB.Account;
using MySqlX.XDevAPI.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

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
                accountDAL.DropAccountByAccountID(1);
                accountDAL.DropAccountByAccountID(2);

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

        [TestMethod]
        public void Deposit_ShouldSucceed()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);

                // Act
                bool result = accountDAL.Deposit(1000001, 100.0); // Assuming a valid account ID

                // Assert
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }



        [TestMethod]
        public void Deposit_ShouldFail_NonExistentID()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);

                // Act
                bool result = accountDAL.Deposit(0, 100.0); // Assuming an invalid account ID

                // Assert
                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }



        [TestMethod]
        public void Withdraw_ShouldSucceed()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);

                // Act
                bool result = accountDAL.Withdraw(1000001, 50.0); // Assuming a valid account ID and amount

                // Assert
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }



        [TestMethod]
        public void Withdraw_WithInsufficientBalance_ShouldFail()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);

                // Act
                bool result = accountDAL.Withdraw(1000001, 5500.0); // Using an amount greater than the balance

                // Assert
                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }



        [TestMethod]
        public void Transfer_ValidAccountIDsAndAmount_ReturnsTrue()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
                uint accountIDFrom = 1000001; // Assuming a valid account ID
                uint toAccountID = 2000001; // Assuming a valid account ID
                double amountOfMoney = 30.0; // Assuming a valid amount

                // Act
                bool result = accountDAL.Transfer(accountIDFrom, toAccountID, amountOfMoney);

                // Assert
                Assert.IsTrue(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }




        [TestMethod]
        public void Transfer_InvalidAccountIDsAndAmount_ReturnsFalse()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
                uint accountIDFrom = 0; // Assuming an invalid account ID
                uint toAccountID = 0; // Assuming an invalid account ID
                double amountOfMoney = -30.0; // Assuming an invalid amount

                // Act
                bool result = accountDAL.Transfer(accountIDFrom, toAccountID, amountOfMoney);

                // Assert
                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }



        [TestMethod]
        public void Transfer_InvalidInsufficientBalance_ReturnsFalse()
        {
            try
            {
                // Arrange
                AccountDAL accountDAL = new AccountDAL(TestBanKDBConnection);
                uint accountIDFrom = 1000001;         // Assuming an valid account ID
                uint toAccountID = 2000001;           // Assuming an valid account ID
                double amountOfMoney = 5500.0;       // Assuming an invalid amount


                // Act
                bool result = accountDAL.Transfer(accountIDFrom, toAccountID, amountOfMoney);

                // Assert
                Assert.IsFalse(result);
            }
            catch (Exception ex)
            {
                Assert.Fail($"An exception occurred: {ex.Message}");
            }
        }

    }
}

