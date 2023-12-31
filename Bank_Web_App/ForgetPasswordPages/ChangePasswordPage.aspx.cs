using BankDB.Customer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankWebApp.ForgetPasswordPages
{
    public partial class ResetPasswordPage : System.Web.UI.Page
    {
        /*--------------------------------------------------------------------------------------------------*/
        /***** Database Setting *****************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private string CanadaDBConnection = ConfigurationManager.AppSettings["CanadaDBConnection"];
        private string BankDBConnection = ConfigurationManager.AppSettings["BankDBConnection"];

        private CustomerDAL DAL { get; set; }
        private CustomerEntity Entity { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Instantiate entity and data access layer classes
            DAL = new CustomerDAL(BankDBConnection);
            Entity = new CustomerEntity(CanadaDBConnection);

            // Get the customer email info by session
            string Email = Session["Email"].ToString();

            // Get customer ID by email and reterive all the customer information
            Entity.SetCustomerID(DAL.GetCustomerIDByEmail(Email));
            Entity = DAL.GetCustomerTableById(Entity.GetCustomerID(), CanadaDBConnection);

            // Clear all the error message
            MessageClear();
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Event Handlers *******************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LoginPage.aspx", true);
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            string newPW = NewPW.Text;
            string reenteredPW = ReenteredPW.Text;

            // If the customer entered the current PW in the DB
            // show the error message
            if(Entity.GetEmail() == newPW)
            {
                PWDuplicateErrorMessage.Visible = true;
                return;
            }

            // If the customer new PW and re-entered PW don't match 
            // show the error message
            if (newPW != reenteredPW)
            {
                ReenteredPWErrorMessage.Visible = true;
                return;
            }

            int result;
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Methods **************************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void MessageClear()
        {
            PWDuplicateErrorMessage.Visible = false;
            ReenteredPWErrorMessage.Visible = false;
        }
    }
}