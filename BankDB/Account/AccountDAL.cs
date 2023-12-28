using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
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
         * Function	   : GetCurrentBalance()
         * Description :
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


    }
}
