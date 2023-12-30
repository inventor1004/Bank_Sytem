using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BankDB.Customer;

namespace BankWebApp
{
    public partial class ForgetPasswordPage : System.Web.UI.Page
    {
        CustomerDAL CustomerDAL;
        internal string BanKDBConnection = ConfigurationManager.AppSettings["BankDBConnection"];
        protected void Page_Load(object sender, EventArgs e)
        {
            CustomerDAL = new CustomerDAL(BanKDBConnection);
        }
        
        protected void EmailSubmitBtn_Click(object sender, EventArgs e)
        {
            string inputEmail = EmailInput.Text;

            // Check whether the input email exists in the customer database
            // If the email is found in the database, move to the EmailValidationPage with the customer email info
            if (CustomerDAL.IsEmailExist(inputEmail))
            {
                Session["Email"] = inputEmail;
                Response.Redirect("./EmailValidationPage.aspx", true);
            }
            EmailInvalidErrorMessage.Visible = true;
        }

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LoginPage.aspx", true);
        }
    }
}