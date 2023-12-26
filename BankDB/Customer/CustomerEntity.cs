using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;


namespace BankDB.Customer
{
    public class CustomerEntity
    {
        internal string DBConnection;

        /*--------------------------------------------------------------------------------------------------*/
        /***** Fields in custoemr table *********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private uint CustomerID;
        private string Email;
        private string Password;
        private string FirstName;
        private string LastName;
        private string DateOfBirth;
        private string PostalCode;
        private string Province;
        private string City;
        private string Address;
        private string PhoneNumber;


        /*--------------------------------------------------------------------------------------------------*/
        /***** Constructor **********************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        public CustomerEntity(string dBConnection)
        {
            DBConnection = dBConnection;
        }


        /*--------------------------------------------------------------------------------------------------*/
        /***** SET & GET Methods ****************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
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
            const bool kSuccess = true, kFailure = false; 
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

        /*
         * Function    : SetPassword()
         * Desctription: Check the password(input) and set it to Password data member if an input satisfy the blow conditions
         *               [Conditions]:
         *                  1. The length of the password should be longer than 10
         *                  2. The length of the password should be shorter than 20
         *                  3. The password sould contain at least on lowercase letter, uppercase letter, number, and special character
         * Parameter   : string Email
         * Return      : int  - kSuccess        = 1
         *                    - KTooShort       = -1
         *                    - kTooLong        = -2
         *                    - KInvalidPattern = -3
         */
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


        public string GetLastName() { return this.LastName; }

        /*
         * Function    : SetLastName()
         * Desctription: Set it to FirstName data member if its length is not greater than the maximum size of SQL raw
         * Parameter   : string LastName
         * Return      : int  - kSuccess         = 1
         *                    - KTooLong         = -1
         *                    - kNotBlankAllowed = -2
         */
        public int SetLastName(string lastName)
        {
            const int kSuccess = 1, KTooLong = -1, kNotBlankAllowed = -2;
            const int kMinimumLength = 50;
            if (lastName.Length <= kMinimumLength)
            {
                // Check whether the blank exists in the input or not
                if (!lastName.Contains(' '))
                {
                    this.LastName = lastName;
                    return kSuccess;
                }
                else return kNotBlankAllowed;
            }
            else { return KTooLong; }
        }


        public string GetDateOfBirth()
        {
            return this.DateOfBirth;
        }

        public bool SetDateOfBirth(DateTime dateOfBirth)
        {
            const bool kSuccess = true, kInvalidDate = false;

            // Check whether the dateOfBirth is future date or not
            // If so, return false
            if (dateOfBirth > DateTime.Now)
            {
                return kInvalidDate;
            }

            // type casting dateOfBirth to string & parse only the year-month-date part
            this.DateOfBirth = dateOfBirth.ToString("yyyy-MM-dd");
            return kSuccess;
        }


        public string GetPostalCode() { return this.PostalCode; }


        /*
         * Function    : SetPostalCode()
         * Desctription: Set it to PostalCode data member if input has valid Canadian postal code pattern
         * Parameter   : string postalCode
         * Return      : int  - kSuccess     = 1
         *                    - kInvalidCode = -1
         */
        public int SetPostalCode(string postalCode)
        {
            const int kSuccess = 1, kInvalidCode = -1;
            const int kWithoutSpace = 6, kWithSpace = 7;

            // Add the sapce if the postal code do not have bank
            if(postalCode.Length == kWithoutSpace)
            {
                // StringBuilder: A class in C# used to perform string manipulation operations.
                StringBuilder stringBuilder = new StringBuilder(postalCode);
                postalCode = stringBuilder.Insert(3, ' ').ToString();
            }

            if (postalCode.Length == kWithSpace)
            {
                // Change all lowercase letter to uppercase letter
                postalCode = postalCode.ToUpper();
                if (IsValidPostalCode(postalCode))
                {
                    this.PostalCode = postalCode;
                    return kSuccess;
                }
            }

            return kInvalidCode;
        }


        public string GetProvince() { return this.Province; }

        /*
         * Function    : SetProvince()
         * Desctription: SetProvince() checks whether the input exists in the Province Table of the Canada Database
         *              and returns true after setting if it exists, otherwise returns false.
         * Parameter   : string province
         * Return      : bool  - kSuccess         = true
         *                     - kInvalidProvince = false
         */
        public bool SetProvince(string province)
        {
            const bool kSuccess = true, kInvalidProvince = false;

            // Set the conection condition of MYSQL data server
            // & Ready to excute quary command
            MySqlConnection connection = new MySqlConnection(DBConnection);
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader reader;
            command.Connection = connection;

            if (string.IsNullOrEmpty(province))
            {
                return kInvalidProvince;
            }

            // SQL Syntax
            // >> Retrive all the province data from SQL Canda Database
            string sqlCmd = "SELECT Province_Name FROM province;";

            try
            {
                // Open the connection and read all province data
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    // Check whether the province name is valid or not
                    if (reader["Province_Name"].ToString().ToLower() == province.ToLower())
                    {
                        // When the input province is validated, set the input
                        this.Province = reader["Province_Name"].ToString();
                        return kSuccess;
                    }
                }
                
            }
            catch (Exception ex)
            {
                Logger.Log("An exception occurred: " + DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return kInvalidProvince;
        }


        public string GetCity() { return this.City; }
        /*
         * Function    : SetProvince()
         * Desctription: SetProvince() checks whether the input exists in the Province Table of the Canada Database
         *              and returns true after setting if it exists, otherwise returns false.
         * Parameter   : string province
         * Return      : bool  - kSuccess         = true
         *                     - kInvalidProvince = false
         */
        public bool SetCity(string city)
        {
            const bool kSuccess = true, kInvalidProvince = false;

            // Set the conection condition of MYSQL data server
            // & Ready to excute quary command
            MySqlConnection connection = new MySqlConnection(DBConnection);
            MySqlCommand command = new MySqlCommand();
            MySqlDataReader reader;
            command.Connection = connection;


            // SQL Syntax
            // >> Retrive all the province data from SQL Canda Database
            string sqlCmd = "SELECT City_Name FROM City;";

            try
            {
                // Open the connection and read all province data
                command.CommandText = sqlCmd;
                connection.Open();
                reader = command.ExecuteReader();


                while (reader.Read())
                {
                    // Check whether the province name is valid or not
                    if (reader["City_Name"].ToString().ToLower() == city.ToLower())
                    {
                        // When the input province is validated, set the input
                        this.City = reader["City_Name"].ToString();
                        return kSuccess;
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log("An exception occurred: " + DateTime.Now.ToString() + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return kInvalidProvince;
        }


        public string GetAddress() { return this.Address; }
        public bool SetAddress(string address)
        {
            const bool kSuccess = true, kInvalidAddress = false;
            const int maxLength = 320;
            if(address.Length <= maxLength)
            {
                this.Address = address;
                return kSuccess;
            }

            return kInvalidAddress;
        }

        public string GetPhoneNumber() { return this.PhoneNumber; }
        public bool SetPhoneNumber (string phoneNumber)
        {
            const bool kSuccess = true, kInvalidNumber = false;
            const int numberLength = 10;
            if (phoneNumber.Length == numberLength)
            {
                if(int.TryParse(phoneNumber, out int result))
                {
                    this.PhoneNumber = phoneNumber;
                    return kSuccess;
                }
                else return kInvalidNumber;
            }
            return kInvalidNumber;
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
            /* Password Regular Expression
             * ^          : Indecates the start of string
             * (?= ...)   : Indicates a lookahead, checking each condition.
             * (?=.*[a-z]): Must contain at least one lowercase letter
             * (?=.*[A-Z]): Must contain at least one uppercase letter
             * (?=.*\d)   : Must contain at least one number
             * .{10,}     : Must be a string of at least 10 characters
             * $: Indecates the end of string
             */
            string patternLowercase   = @"^(?=.*[a-z])";
            string patternUppercase   = @"^(?=.*[A-Z])";
            string patternDigit       = @"^(?=.*\d)";
            string patternSpecialChar = "!@#$%^&*()-_+=";
            string patternLength      = @"^.{10,}$";

            
            // Make regular expression for each pattern
            Regex regexLowercase   = new Regex(patternLowercase);
            Regex regexUppercase   = new Regex(patternUppercase);
            Regex regexDigit       = new Regex(patternDigit);
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


        static bool IsValidPostalCode(string postalCode)
        {
            /* Postal Code Regular Expression (A1A 1A1)
             * ^[A-Z] : String starts with an uppercase letter
             *   \d   : A number appears
             *   [A-Z]: Uppercase alphabet appears
             *        : A space appears
             *   \d   : A number appears
             *   [A-Z]: Uppercase alphabet appears
             *   \d$  : ends with a number
             */
            string CanadaPostalCodePattern = "^[A-Z]\\d[A-Z] \\d[A-Z]\\d$";

            // Make regular expression for each pattern
            Regex regexPostalCode = new Regex(CanadaPostalCodePattern);

            // return the result
            return regexPostalCode.IsMatch(postalCode);
        }
    }
}
