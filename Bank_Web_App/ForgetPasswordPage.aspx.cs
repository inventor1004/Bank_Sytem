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
            if(CustomerDAL.IsEmailExist(inputEmail))
            {
                Response.Redirect("/EmailValidationPage.aspx", true);
            }
            EmailInvalidErrorMessage.Visible = true;
        }
    }
}