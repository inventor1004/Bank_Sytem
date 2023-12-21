using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net.Mail;


namespace BankDB.Customer
{
    public class CustomerEntity
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

        public uint GetCustomerID() { return this.CustomerID; }

        /*
         * Function    : SetCustomerID()
         * Desctription: Check the customerID and set it to Password datamember if its length is not 
         *              greater than the maximum representable of SQL Table
         * Parameter   : uint customerID
         * Return      : bool - kSuccess = true
         *                    - kFailure = false
         */
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


        public string GetEmail() { return this.Email; }

        /*
         * Function    : SetEmail()
         * Desctription: Check the customerID and set it to Email data member if its length is not 
         *              greater than the maximum representable of SQL Table and follow the basic email address format
         * Parameter   : string Email
         * Return      : int  - kSuccess        = 1
         *                    - KOutOfRange     = -1
         *                    - KInvalidPattern = -2
         */
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


        public string GetPassword() { return this.Password; }

        public int SetPassword(string password)
        {
            const int kSuccess = 1, KTooShort = -1, kTooLong = -2, KInvalidPattern = -3;
            const int kMinimumLength = 10, KMaximumNum = 20;
            // check whether the passowrd is too short or not
            if (password.Length >= kMinimumLength)
            {
                // check whether the passowrd is too long or not
                if (password.Length <= KMaximumNum)
                {
                    // check whether the passowrd meets pattern
                    if (IsValidPassword(password))
                    {
                        // Set the password and return success
                        this.Password = password;  
                        return kSuccess;
                    }
                    else return KInvalidPattern;
                }
                else return kTooLong;
            }
            else return KTooShort;
        }

        public string GetFirstName() { return this.FirstName; }

        /*
         * Function    : SetFirstName()
         * Desctription: Set it to FirstName data member if its length is not greater than the maximum size of SQL raw
         * Parameter   : string firstName
         * Return      : int  - kSuccess     = 1
         *                    - KTooLong     = -1
         */
        public int SetFirstName(string firstName) 
        {
           const int kSuccess = 1, KTooLong = -1;
           const int kMinimumLength = 50;
           if (firstName.Length <= kMinimumLength)
           {
               this.FirstName = firstName;
               return kSuccess;
           }
           else { return KTooLong; }
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

        static bool IsValidPassword(string Password)
        {
            // ^: Indecates the start of string
            // (?= ...): Indicates a lookahead, checking each condition.
            // (?=.*[a-z])
            //   >> Must contain at least one lowercase letter
            // (?=.*[A-Z])
            //   >> Must contain at least one uppercase letter
            // (?=.*\d)
            //   >> Must contain at least one number
            // (?=.*[!@#$%^&*()-_+=])
            //   >> It must contain at least one special character
            // .{10,}
            //   >> Must be a string of at least 10 characters
            // $: Indecates the end of string
            string patternLowercase   = @"^(?=.*[a-z])";
            string patternUppercase   = @"^(?=.*[A-Z])";
            string patternDigit       = @"^(?=.*\d)";
            string patternSpecialChar = "!@#$%^&*()-_+=";
            string patternLength      = @"^.{10,}$";

            
            // Make regular expression for each pattern
            Regex regexLowercase   = new Regex(patternLowercase);
            Regex regexUppercase   = new Regex(patternUppercase);
            Regex regexDigit       = new Regex(patternDigit);
            Regex regexSpecialChar = new Regex(patternSpecialChar);
            Regex regexLength      = new Regex(patternLength);


            // check each pattern's condition
            bool hasLowercase   = regexLowercase.IsMatch(Password);
            bool hasUppercase   = regexUppercase.IsMatch(Password);
            bool hasDigit       = regexDigit.IsMatch(Password);
            bool hasSpecialChar = false;

            // Check whether the password contains special character or not
            foreach (char c in Password)
            {
                if (patternSpecialChar.Contains(c))
                {
                    hasSpecialChar = true;
                }
            }

            // Make sure password follows all the pattern
            bool isValidPassword = hasLowercase && hasUppercase && hasDigit && hasSpecialChar && regexLength.IsMatch(Password);
            return isValidPassword;
        }
    }
}
