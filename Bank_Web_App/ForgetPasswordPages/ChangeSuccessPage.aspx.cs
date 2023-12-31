using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BankWebApp.ForgetPasswordPages
{
    public partial class ChangeSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /*--------------------------------------------------------------------------------------------------*/
        /***** Event Handlers *******************************************************************************/
        /*--------------------------------------------------------------------------------------------------*/
        protected void HomeBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LoginPage.aspx", true);
        }

        protected void ToLoginPageBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("../LoginPage.aspx", true);
        }
    }
}