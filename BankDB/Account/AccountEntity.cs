using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB.Account
{
    internal class AccountEntity
    {
        public enum TypeOfAccount { chequing, savings };
        internal string DBConnection;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Fields in custoemr table *********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private uint AccountID;
        private uint AccountNumber;
        private uint CustomerID;
        private double Balance;
        private TypeOfAccount AccountType;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Constructor **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public AccountEntity(string dBConnection)
        {
            DBConnection = dBConnection;
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** SET & GET Methods ****************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public uint GetAccountID() { return this.AccountID; }
        public uint GetAccountNumber() { return this.AccountNumber; }
        public uint GetCustomerID() { return this.CustomerID; }
        public double GetBalance() { return this.Balance;}
        public TypeOfAccount GetAccountType() { return this.AccountType; }

    }
}
