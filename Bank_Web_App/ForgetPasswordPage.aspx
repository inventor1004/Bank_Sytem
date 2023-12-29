<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgetPasswordPage.aspx.cs" Inherits="BankWebApp.ForgetPasswordPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link rel="StyleSheet" href="./css/ForgetPasswordPageStyle.css"/>
     <script>
         var minutes = 5;
         var seconds = 0;

         function updateTimer() {
             var timerElement = document.getElementById("timer");

             if (seconds > 0) {
                 seconds--;
             } else {
                 if (minutes > 0) {
                     minutes--;
                     seconds = 59;
                 }
                 else {
                     clearInterval(timerInterval);
                     timerElement.innerHTML = "Time Out, please re-request the code";
                     return;
                 }
             }

             var formattedTime = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
             timerElement.innerHTML = formattedTime;
         }

         var timerInterval = setInterval(updateTimer, 1000); 
</script>
     <title> Foget Password Page </title>
</head>
<body>
    <form id="ForGetPassword" runat="server">
        <div id="Framework">
            <img id="LogoImage" src="./css/Logo.png"/>

            <div id="insturcution">
                <div id="InsturctionMessage">
                    <h>Reset Your Password</h>
                </div>
                <div id="InstructionDescription">
                    <h>Please provice your email address that you used when you signed up for your account</h>
                </div>
            </div>
            
            <div id="InputArea">
                <div id="EmailInput">
                    <div id="EmailInstruction">
                        <h>Email Address</h>
                    </div>
                    
                    <asp:TextBox runat="server" id="Email" placeholder="Enter your email address"></asp:TextBox>
                    <div>
                        <asp:Button runat="server" ID="EmailSubmitBtn" Text="submit" OnClick="EmailSubmitBtn_Click"/>
                    </div>                    
                </div>
                <div id="ValidationCodeInput">
                    <h2>A validation code has been sent to your email</h2>
                    <asp:TextBox runat="server" id="ValidationCode" placeholder="Enter your email address"></asp:TextBox>
                    <asp:Button runat="server" ID="CodeSubmitBtn" Text="submit" />
                    <div id="timer" runat="server"></div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
