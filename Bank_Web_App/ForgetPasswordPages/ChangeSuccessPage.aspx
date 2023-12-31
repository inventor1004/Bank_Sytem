<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangeSuccessPage.aspx.cs" Inherits="BankWebApp.ForgetPasswordPages.ChangeSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="StyleSheet" href="../css/ChangeSuccessPageStyle.css"/>
    <title></title>
</head>
<body>
    <form id="ChangeSuccessPage" runat="server">
        <asp:ImageButton ID="HomeBtn" runat="server" ImageUrl="../css/homeButton.png" OnClick="HomeBtn_Click" />
        <div id="Framework">
            <div><h1 id="PWChangeInstructions">Password was successfully changed</h1></div>
            <div><h2 id="GoToLoginPageInstructions">Please log in using a new password</h2></div>
            <div><asp:Button runat="server" ID="ToLoginPageBtn" Text="Go back to the login page" OnClick="ToLoginPageBtn_Click"/></div>
        </div>
    </form>
</body>
</html>
