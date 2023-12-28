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

        /*--------------------------------------------------------------------------------------------------*/
        /***** Fields in custoemr table *********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private uint AccountID { get; set; }
        private uint AccountNumber { get; set; }
        private uint CustomerID { get; set; }
        private double Balance { get; set; }
        private TypeOfAccount AccountType { get; set; }



    }
}
