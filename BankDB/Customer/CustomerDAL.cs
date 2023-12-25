using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;


namespace BankDB.Customer
{
    internal class CustomerDAL
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



    }
}
