<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePasswordPage.aspx.cs" Inherits="BankWebApp.ForgetPasswordPages.ResetPasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="StyleSheet" href="../css/ChangePasswordPageStyle.css"/>
    <title>ChangePasswordPage</title>
</head>
<body>
    <form id="ChangePasswordPage" runat="server">
       <asp:ImageButton ID="HomeBtn" runat="server" ImageUrl="../css/homeButton.png" OnClick="HomeBtn_Click" />
       <div id="Framework">
           <img id="LogoImage" src="../css/Logo.png"/>

           <div>                
               <div id="NewPasswordInstruction">
                <h>New Password</h>
                <div>
                <asp:TextBox runat="server" id="NewPW" placeholder="Enter your new password"></asp:TextBox>
                <div>
                    <asp:Label ID="PWPatternErrorMessage" runat="server" 
                    Text ="Password should be inclueded at least one uppercase letter, lowercase letter, number, and special character"
                    Visible = "false" style="font-size:20px" ForeColor="red"></asp:Label>
                </div>
                <div>
                    <asp:Label ID="PWDuplicateErrorMessage" runat="server" 
                     Text = "Password should be inclueded at least one uppercase letter, lowercase letter, number, and special character"
                     Visible = "false" style="font-size:20px" ForeColor="red"></asp:Label>
                </div>
                  </div>
               </div>       
               
               <div id="ReenterPasswordInstruction">
                    <h>Re-enter Password</h>
               </div>       
                    <asp:TextBox runat="server" id="ReenteredPW"  placeholder="Enter your password again"></asp:TextBox>                            
                    <asp:Label ID="ReenteredPWErrorMessage" runat="server" 
                       Text ="Passwords do not match"
                       Visible="false" style="font-size:20px" ForeColor="red"></asp:Label>
               <div>                  
               </div>          
               <asp:Button runat="server" ID="SubmitBtn" Text="Submit" OnClick="SubmitBtn_Click"/>  
           </div>
        </div>
    </form>
</body>
</html>
