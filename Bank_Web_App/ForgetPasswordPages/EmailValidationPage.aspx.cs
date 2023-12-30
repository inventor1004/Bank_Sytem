using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankWebApp
{
    public partial class EmailValidaionPage : System.Web.UI.Page
    {
        private string OTPNumber { get; set; }
        private string Email { get; set; }
        internal const int kLengthOfOTP = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check whether it is the first page load or not
            // If so, generate the OTP number and get the customer's email address using session
            if (Session["OTPNumber"] == null)
            {
                // If it is first page load, generate the OTP number and add as a session
                OTPNumber = BankSystem.GenerateOTP(kLengthOfOTP);
                Session["OTPNumber"] = OTPNumber;
                Email = Session["Email"].ToString();                
            }
            else
            {
                // Get email and OTP number value by session
                Email = Session["Email"].ToString();
                OTPNumber = Session["OTPNumber"].ToString();
                MessageClear();
            }
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Event Handlers *******************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LoginPage.aspx", true);
        }

        protected void CodeSubmitBtn_Click(object sender, EventArgs e)
        {
            string userInputCode = ValidationCodeInput.Text;

            if(userInputCode.Length == 0 || userInputCode.Length > kLengthOfOTP)
            {
                CodeInvalidErrorMessage.Visible = true;
                ValidationInstructionMessage.Visible = false;
                return;
            }

            // If the customer enters the currect OTP number, go to the Change Password Page
            if (userInputCode == OTPNumber)
            {
                Response.Redirect("./ChangePasswordPage.aspx", true);
            }
        }

        protected void CodeResendBtn_Click(object sender, EventArgs e)
        {
            // Get new OTP number and update the session value
            OTPNumber = BankSystem.GenerateOTP(kLengthOfOTP);
            Session["OTPNumber"] = OTPNumber;
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Methods **************************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void MessageClear()
        {
            ValidationInstructionMessage.Visible = false;
            CodeInvalidErrorMessage.Visible = false;
        }

    }
}