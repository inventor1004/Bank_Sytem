using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankDB.Account
{
    internal class AccountEntity
    {
        internal string DBConnection;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Fields in custoemr table *********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        

        /*--------------------------------------------------------------------------------------------------*/
        /***** Constructor **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public AccountEntity(string dBConnection)
        {
            DBConnection = dBConnection;
        }
    }
}
