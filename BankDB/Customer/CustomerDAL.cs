﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;


namespace BankDB.Customer
{
    public class CustomerDAL
    {
        internal MySqlConnection Connection;
        internal MySqlCommand Command;
        internal MySqlDataReader Reader;

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

        /*
         * Function	   : public int CreateNewAccount(CustomerEntity ce)
         * Description : This method add new colums to the customer table in SQL
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
            const int kEmailMaxSize = 50, kPasswordMaxSize = 255, kFistNameMaxSize = 255, kLastNameMaxSize = 255,
                      kPostalCodeMaxSize = 7, kProvinceMaxSize = 25, kCityMaxSize = 25, kAddressMaxSize = 255, kPhoneNumberMaxSize = 10;
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
                               + "VALUES(@Email,"
                                      + "@Password,"
                                      + "@FirstName,"
                                      + "@LastName,"
                                      + "@DateOfBirth,"
                                      + "@PostalCode,"
                                      + "@Province,"
                                      + "@City,"
                                      + "@Address,"
                                      + "@PhoneNumber);";

            try
            {
                // add the values to tje sql database 
                Command.CommandText = sqlCmd;
                Connection.Open();
                int result = Command.ExecuteNonQuery();
                if (result != 0)
                {
                    return kSuccess;
                }

            }
            catch (Exception ex)
            {
                Logger.Log(DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                Connection.Close();
            }

            return kSQLError;
        }

    }
}
