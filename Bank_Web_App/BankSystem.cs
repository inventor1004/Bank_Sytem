using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace BankWebApp
{
    public class BankSystem
    {
        public static string GenerateOTP(int length)
        {

            // Create the instance to generate random number
            Random random = new Random();

            // Create a StringBuilder to store the OTP code
            StringBuilder otp = new StringBuilder();

            // Generates a random number of the specified length and adds it to the OTP code
            for (int i = 0; i < length; i++)
            {
                // Generate random characters within ASCII range and add them to OTP
                char randomChar = (char)random.Next(48, 58); // ASCII numeric range (48-57)
                otp.Append(randomChar);
            }

            // Return the generated OTP code
            return otp.ToString();
        }
    }
}