using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BankDB.Account
{
    public class AccountDAL
    {
        /*--------------------------------------------------------------------------------------------------*/
        /***** MySQL Connection Settings ********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        internal MySqlConnection Connection;
        internal MySqlCommand Command;
        internal MySqlDataReader Reader;

        TransactionDAL transactionDAL;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Constructor **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public AccountDAL(string dBConnection)
        {

            /*
             * 1. Connect to the MySql database based on the given connectionString
             * 2. Create the instance of the MySql for excuttion of the MySql command later
             * 3. Connect the command instance and the MySql instance
             */
            this.Connection = new MySqlConnection(dBConnection);
            this.Command = new MySqlCommand();
            Command.Connection = Connection;

            transactionDAL = new TransactionDAL(dBConnection);
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Data Access Methods **************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        /*
         * Function	   : CreateNewAccount()
         * Description : Create two accounts(savings and chequing) based on the input customerID
         * Parameters  : uint customerID
         * Return      : bool - kSuccess = true
         *                    - kFailure = false
         */
        public bool CreateNewAccount(uint customerID)
        {
            const bool kSuccess = true, kFailure = false;
            if (customerID == 0)
            {
                return kFailure;
            }

            // Check whether the parameter of the CustomerID already exist or not
            if(Command.Parameters.Contains("CustomerID"))
            {
                Command.Parameters["CustomerID"].Value = customerID;
            }
            else
            {
                Command.Parameters.Add("CustomerID", MySqlDbType.Int64).Value = customerID;
            }

            // Chequing Account Number starts with 1
            // Savings Account Number starts with 2
            // Account Number starts ends with the customerID
            uint chequingAccountNumber = 1000000 + customerID;
            uint savingsAccountNumber  = 2000000 + customerID;
            
            if (Command.Parameters.Contains("ChequingAccountNumber"))
            {
                Command.Parameters["ChequingAccountNumber"].Value = chequingAccountNumber;
            }
            else
            {
                Command.Parameters.Add("ChequingAccountNumber", MySqlDbType.Int64, 7).Value = chequingAccountNumber;
            }

            if (Command.Parameters.Contains("SavingsAccountNumber"))
            {
                Command.Parameters["SavingsAccountNumber"].Value = savingsAccountNumber;
            }
            else
            {
                Command.Parameters.Add("SavingsAccountNumber", MySqlDbType.Int64, 7).Value = savingsAccountNumber;
            }

            // SQL Command
            // >> Create new raws for chequing and savings account
            string sqlCmd = "INSERT INTO Account (AccountNumber, CustomerID, Balance, AccountType)"
                                       + "VALUES(@ChequingAccountNumber, @CustomerID, 0.0, 'chequing'),"
                                       + "      (@SavingsAccountNumber , @CustomerID, 0.0, 'savings');";

            try
            {
                // Connect to the sql server and excute the command 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result > 0)
                {
                    return kSuccess;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ": " + ex.Message);       
            }
            finally
            {
                Connection.Close();
            }

            return kFailure;
        }

        /*
         * Function	   : DropAccountByAccountID()
         * Description : Delete an account based on the input AccountID
         * Parameters  : uint customerID
         * Return      : bool - kSuccess = true
         *                    - kFailure = false
         */
        public bool DropAccountByAccountID(uint accountID)
        {
            const bool kSuccess = true, kFailure = false;
            if (accountID == 0)
            {
                return kFailure;
            }

            // Check whether the parameter of the CustomerID already exist or not
            if (Command.Parameters.Contains("AccountID"))
            {
                Command.Parameters["AccountID"].Value = accountID;
            }
            else
            {
                Command.Parameters.Add("AccountID", MySqlDbType.Int64, 7).Value = accountID;
            }


      
            // SQL Command
            // >> Create new raws for chequing and savings account
            string sqlCmd = "DELETE FROM Account WHERE AccountID = @AccountID;";

            try
            {
                // Connect to the sql server and excute the command 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result > 0)
                {
                    SystemMonitor.Log(DateTime.Now.ToString() + $": The account of account ID {accountID} is deleted.");
                    return kSuccess;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ": " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return kFailure;
        }


        /*
         * Function	   : DropAccountByAccountID()
         * Description : Delete an account based on the input AccountID
         * Parameters  : uint customerID
         * Return      : bool - kSuccess = true
         *                    - kFailure = false
         */
        public bool DropAccountByCustomerID(uint customerID)
        {
            const bool kSuccess = true, kFailure = false;
            if (customerID == 0)
            {
                return kFailure;
            }

            // Check whether the parameter of the CustomerID already exist or not
            if (Command.Parameters.Contains("CustomerID"))
            {
                Command.Parameters["CustomerID"].Value = customerID;
            }
            else
            {
                Command.Parameters.Add("CustomerID", MySqlDbType.Int64, 7).Value = customerID;
            }



            // SQL Command
            // >> Create new raws for chequing and savings account
            string sqlCmd = "DELETE FROM Account WHERE CustomerID = @CustomerID;";

            try
            {
                // Connect to the sql server and excute the command 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result > 0)
                {
                    SystemMonitor.Log(DateTime.Now.ToString() + $": The accounts of customerID = {customerID} are deleted.");
                    return kSuccess;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ": " + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return kFailure;
        }

        /*
         * Function	   : GetCurrentBalance()
         * Description : It  takes AccountID as a parameter and return the current balance of the selected account
         * Parameters  : uint accountNumber
         * Return      : double - CurrentBalance: success
         *                      - double.NaN    : fail                        
         *              
         */
        public double GetCurrentBalance(uint accountNumber)
        {
            const double kFailure = double.NaN;
            if (accountNumber == 0)
            {
                return kFailure;
            }

            // Check whether the parameter of the AccountNumber already exist or not
            if (Command.Parameters.Contains("AccountNumber"))
            {
                Command.Parameters["AccountNumber"].Value = accountNumber;
            }
            else
            {
                Command.Parameters.Add("AccountNumber", MySqlDbType.Double).Value = accountNumber;
            }

            // SQL Command
            // >> Retrieve a current balance where from a matched account number
            string sqlCmd = "SELECT Balance FROM Account WHERE AccountNumber = @AccountNumber;";

            try
            {
                Command.CommandText = sqlCmd;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();
                if (Reader["Balance"] != DBNull.Value)
                {
                    return double.Parse(Reader["Balance"].ToString());
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ": " + ex.Message);
            }
            return kFailure;
        }


        /*
        * Function	  : IsEnoughBalance()
        * Description : This function check whether the selected account has enough money money to witdraw or not
        * Parameters  : int accountID, double amountOfMoney
        * Return      : bool false - process failed 
        *                    true  - process success            
        */
        public bool IsEnoughBalance(uint accountNumber, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;
            const int kMaxDecimalPoint = 2;

            // kMaxDecimalPoint
            amountOfMoney = Math.Round(amountOfMoney, kMaxDecimalPoint);

            // Check wether the inputs are positive interger or not
            if (accountNumber < 0)
            {
                return kProcessFailed;
            }

            // Parameterize the input to avoid SQL injections
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the accountIDFrom
            if (Command.Parameters.Contains("@AccountNumber"))
            {
                Command.Parameters["@AccountNumber"].Value = accountNumber;
            }
            else
            {
                Command.Parameters.Add("@AccountNumber", MySqlDbType.Int64).Value = accountNumber;
            }

            // Parameterize the amountOfMoney
            if (Command.Parameters.Contains("@AmountOfMoney"))
            {
                Command.Parameters["@AmountOfMoney"].Value = amountOfMoney;
            }
            else
            {
                Command.Parameters.Add("@AmountOfMoney", MySqlDbType.Double).Value = amountOfMoney;
            }


            // SQL Syntax
            // >> Check whether the sender's account exist and it has enough balance to withdraw or not
            string sqlCmdFindID = "SELECT AccountNumber, Balance FROM `Account` WHERE AccountNumber = @AccountNumber;";

            try
            {
                Command.CommandText = sqlCmdFindID;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();

                // Check whether the requested accountIDFrom exist or not in Account TABLE
                // If not, return false
                if (Reader["accountNumber"].ToString() == accountNumber.ToString())
                {
                    // check whether the current balance is enough to withdraw for transfering
                    // If not, return false
                    string currentBalance = Reader["Balance"].ToString();
                    if (double.Parse(currentBalance) >= amountOfMoney)
                    {
                        return kProcessSuccess;
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return kProcessFailed;
        }


        /*
         * Function	   : IsAccountExist()
         * Description : This function check whether the input accountID exists or not
         * Parameters  : int accountID
         * Return      : bool false - process failed 
         *                    true  - process success            
         */
        public bool IsAccountExist(uint accountNumber)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // Check wether the inputs are positive interger or not
            if (accountNumber < 0)
            {
                return kProcessFailed;
            }


            // Parameterize the input to avoid SQL injections
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the accountIDFrom
            if (Command.Parameters.Contains("@AccountNumber"))
            {
                Command.Parameters["@AccountNumber"].Value = accountNumber;
            }
            else
            {
                Command.Parameters.Add("@AccountNumber", MySqlDbType.Int64).Value = accountNumber;
            }



            // SQL Syntax
            // >> Check whether the sender's account exist and it has enough balance to withdraw or not
            string sqlCmdFindID = "SELECT AccountNumber FROM `Account` WHERE AccountNumber = @AccountNumber;";

            try
            {
                Command.CommandText = sqlCmdFindID;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();

                // Check whether the requested accountID exist or not in Account TABLE
                // If not, return false
                if (Reader["accountNumber"].ToString() == accountNumber.ToString())
                {
                    return kProcessSuccess;
                }
                else
                {
                    return kProcessFailed;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return kProcessFailed;
        }


        /*
         * Function	   : Deposit()
         * Description : Add the requested money to the current balance in selected account
         * Parameters  : int accountID        - Customer's accounID to specify the account 
         *               double amountOfMoney - The amount of money the cusomer wants to depoist
         * Return      : bool false - process failed 
         *                    true  - process success
         */
        public bool Deposit(uint accountNumber, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // Check wether the inputs are positive interger or not
            if (accountNumber < 0 || amountOfMoney < 0)
            {
                return kProcessFailed;
            }

            // Parameterize the input to avoid SQL injections
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the accountID
            if (Command.Parameters.Contains("@AccountNumber"))
            {
                Command.Parameters["@AccountNumber"].Value = accountNumber;
            }
            else
            {
                Command.Parameters.Add("@AccountNumber", MySqlDbType.Int64).Value = accountNumber;
            }

            // Parameterize the amountOfMoney
            if (Command.Parameters.Contains("@AmountOfMoney"))
            {
                Command.Parameters["@AmountOfMoney"].Value = amountOfMoney;
            }
            else
            {
                Command.Parameters.Add("@AmountOfMoney", MySqlDbType.Double).Value = amountOfMoney;
            }

            // SQL Syntax
            // >> update the balance from the Account table based on the AccountID
            string sqlCmd = "UPDATE `Account` SET Balance = Balance + @amountOfMoney WHERE accountNumber = @AccountNumber;";

            try
            {
                Command.CommandText = sqlCmd;
                Connection.Open();

                int result = Command.ExecuteNonQuery();
                if (result == 0)
                {
                    return kProcessFailed;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            // add new transaction recode to the transaction table
            transactionDAL.AddNewTransaction(accountNumber, accountNumber, "deposit", amountOfMoney);
            return kProcessSuccess;
        }


        /*
         * Function	  : Withdraw()
         * Description : Wihdraw the requested money to the current balance in selected account
         * Parameters  : int accountID        - Customer's accounID to specify the account 
         *               double amountOfMoney - The amount of money the cusomer wants to witdraw
         * Return      : bool false - process failed 
         *                    true  - process success
         */
        public bool Withdraw(uint accountNumber, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;
            //const int kMaxDecimalPoint = 2;

            // Check wether the inputs are positive interger or not
            // amountOfMoney = Math.Round(amountOfMoney, kMaxDecimalPoint);

            // Check wether the inputs are positive interger or not
            if (accountNumber < 0 || amountOfMoney < 0)
            {
                return kProcessFailed;
            }

            // Parameterize the input to avoid SQL injections
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the accountID
            if (Command.Parameters.Contains("@AccountNumber"))
            {
                Command.Parameters["@AccountNumber"].Value = accountNumber;
            }
            else
            {
                Command.Parameters.Add("@AccountNumber", MySqlDbType.Int64).Value = accountNumber;
            }

            // Parameterize the amountOfMoney
            if (Command.Parameters.Contains("@amountOfMoney"))
            {
                Command.Parameters["@AmountOfMoney"].Value = amountOfMoney;
            }
            else
            {
                Command.Parameters.Add("@AmountOfMoney", MySqlDbType.Double).Value = amountOfMoney;
            }



            // SQL Syntax
            // >> Retrieve the requested AccountID
            string sqlCmd = "SELECT AccountNumber, Balance FROM Account WHERE AccountNumber = @AccountNumber;";


            try
            {
                Command.CommandText = sqlCmd;
                Connection.Open();
                Reader = Command.ExecuteReader();

                // Read all data from the customer table
                Reader.Read();

                if (Reader["AccountNumber"].ToString() == accountNumber.ToString())
                {
                    string currentBalance = Reader["Balance"].ToString();
                    // If current balance is lower than requested witdraw money, return false
                    if (double.Parse(currentBalance) < amountOfMoney)
                    {
                        return kProcessFailed;
                    }
                }
                else
                {
                    // If AccountID does not exist in Account TABLE, return false
                    return kProcessFailed;
                }
                // Refresh the conneciton & withdraw requested money
                Connection.Close();

                // SQL Syntax
                // >> update the balance from the Account table based on the AccountID
                sqlCmd = "UPDATE `Account` SET Balance = Balance - @AmountOfMoney WHERE AccountNumber = @AccountNumber;";
                Command.CommandText = sqlCmd;
                Connection.Open();

                int result = Command.ExecuteNonQuery();
                if (result == 0)
                {
                    return kProcessFailed;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            // add new transaction recode to the transaction table
            transactionDAL.AddNewTransaction(accountNumber, accountNumber, "withdraw", amountOfMoney);
            return kProcessSuccess;
        }


        /*
         * Function	   : Transfer()
         * Description : withdraw the money from the selected customer's account 
         *              and deposit the money to the different account
         * Parameters  : int accountIDFrom    - The accountID which the money will be withdrawed
         *               int toAccountID      - The accountID which the money will be depoisted
         *               double amountOfMoney - Requsted transfer money
         * Return      : bool false - process failed 
         *                    true  - process success
         */
        public bool Transfer(uint accountNumberFrom, uint toAccountNumber, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;
            const int kMaxDecimalPoint = 2;

            // kMaxDecimalPoint
            amountOfMoney = Math.Round(amountOfMoney, kMaxDecimalPoint);

            // Check wether the inputs are positive interger or not
            if (accountNumberFrom < 0 || toAccountNumber < 0 || amountOfMoney < 0)
            {
                return kProcessFailed;
            }

            // Parameterize the input to avoid SQL injections
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the accountIDFrom
            if (Command.Parameters.Contains("@AccountNumberFrom"))
            {
                Command.Parameters["@AccountNumberFrom"].Value = accountNumberFrom;
            }
            else
            {
                Command.Parameters.Add("@AccountNumberFrom", MySqlDbType.Int64).Value = accountNumberFrom;
            }

            // Parameterize the toAccountID
            if (Command.Parameters.Contains("@ToAccountNumber"))
            {
                Command.Parameters["@ToAccountNumber"].Value = toAccountNumber;
            }
            else
            {
                Command.Parameters.Add("@ToAccountNumber", MySqlDbType.Int64).Value = toAccountNumber;
            }

            // Parameterize the amountOfMoney
            if (Command.Parameters.Contains("@AmountOfMoney"))
            {
                Command.Parameters["@AmountOfMoney"].Value = amountOfMoney;
            }
            else
            {
                Command.Parameters.Add("@AmountOfMoney", MySqlDbType.Double).Value = amountOfMoney;
            }



            // SQL Syntax
            // >> Check whether the sender's account exist and it has enough balance to withdraw or not
            string sqlCmdFindID = "SELECT AccountNumber, Balance FROM `Account` WHERE AccountNumber = @AccountNumberFrom;";

            // Prepare to witdraw from the sender's account and 
            // to deposit to the receiver's account
            // if the receiver's account exist in the account table
            string sqlCmdTransfer = string.Empty;


            try
            {
                Command.CommandText = sqlCmdFindID;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();

                // Check whether the requested accountIDFrom exist or not in Account TABLE
                // If not, return false
                if (Reader["AccountNumber"].ToString() == accountNumberFrom.ToString())
                {
                    // check whether the current balance is enough to withdraw for transfering
                    // If not, return false
                    string currentBalance = Reader["Balance"].ToString();
                    if (double.Parse(currentBalance) < amountOfMoney)
                    {
                        return kProcessFailed;
                    }
                }
                else
                {
                    return kProcessFailed;
                }


                // SQL Syntax
                // >> Check whether the receiver's account exist or not
                sqlCmdFindID = "SELECT AccountNumber FROM `Account` WHERE AccountNumber = @ToAccountNumber;";
                Command.CommandText = sqlCmdFindID;

                // reset the connection
                Connection.Close();
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();
                int accountID = int.Parse(Reader["AccountNumber"].ToString());

                // if ID and Password are found in the customer table, return the customerID
                // If not, return fail
                if (accountID == toAccountNumber)
                {
                    sqlCmdTransfer = "UPDATE `Account` SET Balance = Balance - @AmountOfMoney WHERE AccountNumber = @AccountNumberFrom;";

                }
                else
                {
                    return kProcessFailed;
                }

                // Check whether the reviever's account exist in the Account table or not
                if (sqlCmdTransfer == string.Empty)
                {
                    return kProcessFailed;
                }
                else
                {
                    // reset the connection
                    Connection.Close();
                    Connection.Open();

                    // If reviever's account exist, start transfering
                    Command.CommandText = sqlCmdTransfer;
                    Command.ExecuteNonQuery();

                    sqlCmdTransfer = "UPDATE `Account` SET Balance = Balance + @amountOfMoney WHERE AccountNumber = @toAccountNumber;";
                    Command.CommandText = sqlCmdTransfer;
                    Command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            // add new transaction recode to the transaction table
            transactionDAL.AddNewTransaction(accountNumberFrom, toAccountNumber, "transfer", amountOfMoney);
            return kProcessSuccess;

        }
    }

}
