using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Mail;
using System.Runtime.Remoting.Messaging;


namespace BankDB.Customer
{
    public class CustomerDAL
    {
        /*--------------------------------------------------------------------------------------------------*/
        /***** MySQL Connection Settings ********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        internal MySqlConnection Connection;
        internal MySqlCommand Command;
        internal MySqlDataReader Reader;

        // Properties' size
        internal const int kEmailMaxSize = 50, kPasswordMaxSize = 255, kFistNameMaxSize = 255, kLastNameMaxSize = 255,
                           kPostalCodeMaxSize = 7, kProvinceMaxSize = 25, kCityMaxSize = 25, kAddressMaxSize = 255, 
                           kPhoneNumberMaxSize = 10;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Constructor **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public CustomerDAL(string connectionString)
        {
            /*
             * 1. Connect to the MySql database based on the given connectionString
             * 2. Create the instance of the MySql for excuttion of the MySql command later
             * 3. Connect the command instance and the MySql instance
             */
            this.Connection = new MySqlConnection(connectionString);   
            this.Command = new MySqlCommand();
            Command.Connection = Connection;
        }


        /*--------------------------------------------------------------------------------------------------*/
        /***** DAL Methods **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/

        /*
         * Function	   : public int CreateNewAccount(CustomerEntity ce)
         * Description : This method add a new colum to the customer table in SQL
         *              All properties should be filled to add the new colum
         * Parameters  : CustomerEntity ce - the object which contains 
         * Return      : int  return 1  : Process succeed 
         *                    return -1 : Null property is found in the CustomerEntity ce
         *                    return -2 : SQL command is failed
         */
        public int CreateNewAccount(CustomerEntity ce)
        {
            const int kSuccess = 1, kNullField = -1, kSQLError = -2;

            // Check whether all datamembers are filled
            // If not, returns false
            if (ce.GetEmail()    == null || ce.GetPassword()    == null || ce.GetFirstName()  == null ||
                ce.GetLastName() == null || ce.GetDateOfBirth() == null || ce.GetPostalCode() == null ||
                ce.GetProvince() == null || ce.GetCity()        == null || ce.GetAddress()    == null || ce.GetPhoneNumber() == null)
            {
                return kNullField;
            }

            // Parameterize the input to avoid SQL injections          
            Command.Parameters.Add("@Email",       MySqlDbType.VarChar, kEmailMaxSize      ).Value = ce.GetEmail();
            Command.Parameters.Add("@Password",    MySqlDbType.VarChar, kPasswordMaxSize   ).Value = ce.GetPassword();
            Command.Parameters.Add("@FirstName",   MySqlDbType.VarChar, kFistNameMaxSize   ).Value = ce.GetFirstName();
            Command.Parameters.Add("@LastName",    MySqlDbType.VarChar, kLastNameMaxSize   ).Value = ce.GetLastName();
            Command.Parameters.Add("@DateOfBirth", MySqlDbType.Date                        ).Value = ce.GetDateOfBirth();
            Command.Parameters.Add("@PostalCode",  MySqlDbType.VarChar, kPostalCodeMaxSize ).Value = ce.GetPostalCode();
            Command.Parameters.Add("@Province",    MySqlDbType.VarChar, kProvinceMaxSize   ).Value = ce.GetProvince();
            Command.Parameters.Add("@City",        MySqlDbType.VarChar, kCityMaxSize       ).Value = ce.GetCity();
            Command.Parameters.Add("@Address",     MySqlDbType.VarChar, kAddressMaxSize    ).Value = ce.GetAddress();
            Command.Parameters.Add("@PhoneNumber", MySqlDbType.VarChar, kPhoneNumberMaxSize).Value = ce.GetPhoneNumber();


            // SQL Syntax
            // Create the query want to add <Note: parameterized data should be not covered by '' in SQL syntax>
            string sqlCmd = "INSERT INTO Customer (Email, Password, FirstName, LastName, DateOfBirth, PostalCode, Province, City, Address, PhoneNumber)"
                               + "VALUES(@Email, @Password, @FirstName, @LastName, @DateOfBirth, @PostalCode, @Province, @City, @Address, @PhoneNumber);";

            try
            {
                // add the values to tje sql database 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result != 0)
                {
                    SystemMonitor.Log(DateTime.Now.ToString() + ": Customer name" + ce.GetFirstName() + " " + ce.GetLastName() + " creates new account.");
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

            return kSQLError;
        }

        /*
        * Function	   : DropCustomerByEmail()
        * Description : Delete an account based on the input AccountID
        * Parameters  : uint customerID
        * Return      : bool - kSuccess = true
        *                    - kFailure = false
        */
        public bool DropCustomerByEmail(string email)
        {
            const bool kSuccess = true, kFailure = false;
            if (string.IsNullOrEmpty(email))
            {
                return kFailure;
            }

            // Check whether the parameter of the CustomerID already exist or not
            if (Command.Parameters.Contains("@Email"))
            {
                Command.Parameters["@Email"].Value = email;
            }
            else
            {
                Command.Parameters.Add("@Email", MySqlDbType.VarChar, kEmailMaxSize).Value = email;
            }



            // SQL Command
            // >> Create new raws for chequing and savings account
            string sqlCmd = "DELETE FROM Customer WHERE Email = @Email;";

            try
            {
                // Connect to the sql server and excute the command 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result > 0)
                {
                    SystemMonitor.Log(DateTime.Now.ToString() + $": The customer account of email address {email} is deleted.");
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
         * Function	   : GetCustomerIDByEmail()
         * Description : Retrieve the CustomerID from the SQL Custoemer table based on the customer's email address           
         * Parameters  : string emailAddress
         * Return      : int (customerID): Process success
         *                   -1: emailAddress is empty
         *                   -2: SQL error
         */
        public int GetCustomerIDByEmail(string emailAddress)
        {
            const int kEmptyString = -1, kSQLError = -2;
            if(string.IsNullOrEmpty(emailAddress))
            {
                return kEmptyString;
            }

            // check whether the property is already parameterized or not
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the Email
            if (Command.Parameters.Contains("@Email"))
            {
                Command.Parameters["@Email"].Value = emailAddress;
            }
            else
            {
                Command.Parameters.Add("@Email", MySqlDbType.VarChar, kEmailMaxSize).Value = emailAddress;
            }

            // SQL Syntax
            // Create the query want to add <Note: parameterized data should be not covered by '' in SQL syntax>
            string sqlCmd = "SELECT CustomerID FROM Customer WHERE Email = @Email;";

            try
            {
                // Retrieve the customerID from the customer table
                Command.CommandText = sqlCmd;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();
                if (Reader["CustomerID"] != DBNull.Value)
                {              
                    return int.Parse(Reader["CustomerID"].ToString());
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

            return kSQLError;
        }


        /*
         * Function	   : List<CustomerEntity> GetCustomerTable()
         * Description : Retrieve all data from the Customer table and return as a List of CustomerEntity
         * Parameters  : int customerID, string connectionSetting
         * Return      : List<CustomerEntity> - Contain all data from the customer table          
         */
        public CustomerEntity GetCustomerTableById(int customerID, string connectionSetting)
        {
            // check whether the property is already parameterized or not
            // >> If the parameter already exist, update the value
            //    If not, create new parameter
            // Parameterize the Email
            if (Command.Parameters.Contains("@CustomerID"))
            {
                Command.Parameters["@CustomerID"].Value = customerID;
            }
            else
            {
                Command.Parameters.Add("@CustomerID", MySqlDbType.VarChar).Value = customerID;
            }

            // SQL Syntax
            // >> Retrive all data from the customer table
            string sqlCmd = "SELECT * FROM CUSTOMER WHERE CustomerID = @CustomerID;";
           
            try
            {
                Command.CommandText = sqlCmd;
                Connection.Open();
                Reader = Command.ExecuteReader();
                Reader.Read();
                
                CustomerEntity entity = new CustomerEntity(connectionSetting);

                // If requested ID exists,
                if (Reader["CustomerID"] != DBNull.Value)
                {
                    // Set all the properties
                    entity.SetCustomerID(uint.Parse(Reader["CustomerID"].ToString()));
                    entity.SetEmail(Reader["Email"].ToString());
                    entity.SetPassword(Reader["Password"].ToString());
                    entity.SetFirstName(Reader["FirstName"].ToString());
                    entity.SetLastName(Reader["LastName"].ToString());
                    entity.SetDateOfBirth(DateTime.Parse(Reader["DateOfBirth"].ToString()));
                    entity.SetPostalCode(Reader["PostalCode"].ToString());
                    entity.SetProvince(Reader["Province"].ToString());
                    entity.SetCity(Reader["City"].ToString());
                    entity.SetAddress(Reader["Address"].ToString());
                    entity.SetPhoneNumber(Reader["PhoneNumber"].ToString());

                    // Check whether all the data members are filled completely
                    if (entity.AreAllPropertiesSet())
                    {
                        return entity;
                    }
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

            return null;
        }
    }
}
