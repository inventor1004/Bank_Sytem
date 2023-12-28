using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB
{
    public class TransactionDAL: TransactionEntity
    {
        internal MySqlConnection connection;
        internal MySqlCommand command;
        internal MySqlDataReader reader;

        public TransactionDAL(string connectionString) 
        {
            connection = new MySqlConnection(connectionString);
            command = new MySqlCommand();
            command.Connection = connection;
        }


        public List<TransactionEntity> GetTransactionDetail()
        {
            string sqlCmd = "SELECT * FROM transaction ORDER BY TransactionDate DESC";

            List<TransactionEntity> dataList = new List<TransactionEntity>();

            try
            {
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();

                // Read all data from the customer table
                while (reader.Read())
                {
                    TransactionEntity entity = new TransactionEntity();
                    entity.TransactionID = reader["TransactionID"].ToString();
                    entity.Amount = reader["Amount"].ToString();
                    entity.TransactionDate = (DateTime)reader["TransactionDate"];
                    entity.TransactionType = reader["TransactionType"].ToString();
                    entity.SenderAccountNumber = reader["SenderAccountID"].ToString();
                    entity.ReceiverAccountNumber = reader["ReceiverAccountID"].ToString() ;
                    dataList.Add(entity);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dataList;
        }


        /*
         * Function	   : AddNewTransaction()
         * Description : Recode transaction history
         * Parameters  : int senderAccountID, int receiverAccountID, string typeOfTransaction, double amountOfMoney
         * Return      : bool true  - process success
         *                    false - process failed 
         */
        public bool AddNewTransaction(uint senderAccountID, uint receiverAccountID, string typeOfTransaction, double amountOfMoney)
        {
            const bool kProcessFailed = false, kProcessSuccess = true;

            // get current time and transfer to 24hr format 
            DateTime dateTime = DateTime.Now;
            string formattedDateTime = dateTime.ToString("yyyy-MM-dd HH:mm:ss");

            // SQL Syntax
            // >> Retrive all data from the customer table
            string sqlCmd = "INSERT INTO `Transaction` (SenderAccountID, ReceiverAccountID, TransactionType, Amount, TransactionDate) " +
                           $"VALUES ({senderAccountID}, {receiverAccountID}, '{typeOfTransaction}', {amountOfMoney}, '{formattedDateTime}');";

            try
            {
                // add the values to tje sql database 
                command.CommandText = sqlCmd;
                connection.Open();

                // check whether the data is successfully added
                int result = command.ExecuteNonQuery();
                if (result == 0)
                {
                    return kProcessFailed;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log(DateTime.Now.ToString() + ex.Message);
                return kProcessFailed;
            }
            finally
            {
                connection.Close();
            }

            return kProcessSuccess;
        }
    }
}
