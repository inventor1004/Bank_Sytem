using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;


namespace BankDB.Customer
{
    internal class CustomerEntity
    {
        /*--------------------------------------------------------------------------------------------------*/
        /***** Fields in custoemr table *********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private uint CustomerID;
        private string Email;
        private string Password;
        private string FirstName;
        private string LastName;
        private string DateOfBirth;
        private string Address;
        private string PhoneNumber;

        /*--------------------------------------------------------------------------------------------------*/
        /***** SET & GET Functions **************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        internal const bool kSuccess = true;
        internal const bool kFailure = false;

        public uint GetCustomerID()
        {
            return this.CustomerID;
        }

        public bool SetCustomerID(uint customerID)
        {
            const int kMaxNum = 2 ^ 32 - 1; // 4294967295
            if (customerID != 0 && customerID < kMaxNum)
            {
                this.CustomerID = customerID;
                return kSuccess;
            }
            return kFailure;
        }


        public string GetEmail()
        {
            return this.Email;
        }
        public int SetEmail(string Email)
        {
            const int kSuccess = 1, KOutOfRange = -1, KInvalidPattern = -2;

            // Check Maximum storage range and Email validation
            const int kMaxRange = 320;
            if (Email.Length <= kMaxRange)
            {
                if (IsValidEmail(Email))
                {
                    // Set the Email
                    this.Email = Email;
                    return kSuccess;
                }
                else return KInvalidPattern;
            }
            else return KOutOfRange;
        }


        /*--------------------------------------------------------------------------------------------------*/
        /***** Validation Methods ***************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        static bool IsValidEmail(string email)
        {
            // ^: Indecates the start of string
            // [a-zA-Z0-9._%+-]: Indecates local part string validation
            //                  >> Lowercase alpha bet, upper case alphabet, sort of special characters(._%+-)
            // @[a-zA-Z0-9.-]: Indecates domain part string validation
            //                  >> Should start '@'
            //                  >> Lowercase alpha bet, upper case alphabet, sort of special characters(._%+-)
            // \.[a-zA-Z]{2,}: Indecates top-level domain validation
            //                  >> Should start '.'
            //                  >> Lowercase alpha bet, upper case alphabet
            //                  >> {2,} indecates at least two characters
            // $: Indecates the end of string
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Pass the validation pattern to the regular expression & return the result
            Regex regex = new Regex(pattern);
            return regex.IsMatch(email);
        }
    }
}
