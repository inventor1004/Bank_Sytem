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

            string sqlCmd = "INSERT INTO Account (AccountNumber, CustomerID, Balance, AccountType)"
                                       + "VALUES(@ChequingAccountNumber, @CustomerID, 0.0, 'chequing'),"
                                       + "      (@SavingsAccountNumber , @CustomerID, 0.0, 'savings');";

            try
            {
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
                ErrorLogger.Log(DateTime.Now.ToString() + ": " + ex.Message);            }

            return kFailure;
        }

    }
}
