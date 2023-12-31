<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPasswordPage.aspx.cs" Inherits="BankWebApp.ForgetPasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="StyleSheet" href="../css/ForgetPasswordPageStyle.css"/>
     <title> Foget Password Page </title>
</head>
<body>
    <form id="ForGetPassword" runat="server">
    <asp:ImageButton ID="HomeBtn" runat="server" ImageUrl="../css/homeButton.png" OnClick="HomeBtn_Click" />
        <div id="Framework">
            <img id="LogoImage" src="../css/Logo.png"/>

            <div id="insturcution">
                <div id="InsturctionMessage">
                    <h>Reset Your Password</h>
                </div>
                <div id="InstructionDescription">
                    <h>Please provice your email address that you used when you signed up for your account</h>
                </div>
            </div>
            
            <div id="InputArea">
                <div id="EmailInputArea">
                    <div id="EmailInstruction">
                        <h>Email Address</h>
                    </div>                   
                        <asp:TextBox runat="server" id="EmailInput" placeholder="Enter your email address"></asp:TextBox>
                    <div style="margin-bottom: 10px;">
                        <asp:Label ID="EmailInvalidErrorMessage" runat="server" Visible="false"
                            Text ="Invalid email address. Please check your email again"></asp:Label>
                        <asp:Button runat="server" ID="EmailSubmitBtn" Text="submit" OnClick="EmailSubmitBtn_Click"/>
                    </div>                        
                    <div>
                    </div>
                </div>              
            </div>
        </div>
    </form>
</body>
</html>
