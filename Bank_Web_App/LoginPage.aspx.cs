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
    public partial class LoginPage : System.Web.UI.Page
    {
        /*--------------------------------------------------------------------------------------------------*/
        /***** MySQL Connection Settings ********************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private string BankDBConnection = ConfigurationManager.AppSettings["BankDBConnection"];

        /*--------------------------------------------------------------------------------------------------*/
        /***** MySQL Data Members ***************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        private CustomerDAL DAL { get; set; }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Page Load ************************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL = new CustomerDAL(BankDBConnection);
            MessageClear();
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Event Handlers *******************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string CustomerEmail = ID.Text;
            string CustomerPW = PW.Text;

            // Check whether the input email exists or not
            if(DAL.IsEmailExist(CustomerEmail))
            {
                // Check whether the input password is mathced or not
                if (DAL.IsPasswordValid(CustomerEmail, CustomerPW))
                {
                    Response.Redirect("./MainPage/MainPage.aspx", true);
                }
            }

            IDPWErrorMessage.Visible = true;
        }

        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/LoginPage.aspx", true);
        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Methods **************************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void MessageClear()
        {
            IDPWErrorMessage.Visible = false;
        }
    }
}