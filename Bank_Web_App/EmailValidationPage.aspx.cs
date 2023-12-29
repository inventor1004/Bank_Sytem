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
        internal string OTPNumber;
        internal const int kLengthOfOTP = 6;
        protected void Page_Load(object sender, EventArgs e)
        {
            OTPNumber = BankSystem.GenerateOTP(kLengthOfOTP);
        }

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/LoginPage.aspx", true);
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
        }

        protected void MessageClear()
        {
            ValidationInstructionMessage.Visible = false;
            CodeInvalidErrorMessage.Visible = false;
        }
    }
}