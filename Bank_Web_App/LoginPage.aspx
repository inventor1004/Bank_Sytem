<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BankWebApp.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html" charset="utf-8" />
    <link rel="StyleSheet" href="./css/LoginPageStyle.css"/>
    <title> Login Page </title> 
</head>
<body>
    <form id="MainPageForm" runat="server">
        <asp:ImageButton ID="HomeBtn" runat="server" ImageUrl="./css/homeButton.png" OnClick="HomeBtn_Click" />
        <div id="LoginFramework">
            <img id="LogoImage" src="./css/Logo.png"/>

            <div id="InputArea">
                <div>
                    <asp:TextBox runat="server" id="ID" placeholder="Enter your ID"></asp:TextBox>
                </div>
                <div>
                    <asp:TextBox runat="server" id="PW" placeholder="Enter your password"></asp:TextBox>
                </div>
            </div>

            <asp:Button runat="server" ID="LoginButton" text="Login" OnClick="LoginButton_Click"/>

            <div>
                <a href="ForgetPasswordPages/ForgetPasswordPage.aspx" id="ForgetPasswordPageLink">Forgot password</a>
                <a href="SignupPage.aspx" id="SignupPageLink">Create new account</a>
            </div>

        </div>       

        
    </form>
</body>
</html>
