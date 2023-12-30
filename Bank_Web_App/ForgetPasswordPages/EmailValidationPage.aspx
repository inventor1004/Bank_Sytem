<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmailValidationPage.aspx.cs" Inherits="BankWebApp.EmailValidaionPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="StyleSheet" href="../css/EmailValidationPageStyle.css"/>
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
     

        function TimerReset() {            minutes = 5
            seconds = 0;
            var timerElement = document.getElementById("timer");
            var formattedTime = minutes + ":" + (seconds < 10 ? "0" : "") + seconds;
            timerElement.innerHTML = formattedTime;erHTML = formattedTime;
        }

        var timerInterval = setInterval(updateTimer, 1000);    
    
    </script>
    <title></title>
</head>
<body>
    <form id="EmailValidaionPage" runat="server">
        <asp:ImageButton ID="HomeBtn" runat="server" ImageUrl="../css/homeButton.png" OnClick="HomeBtn_Click" />
        <div id="Framework">
            <img id="LogoImage" src="../css/Logo.png"/>
            <div id="ValidationCodeArea">                
                <div id="ValidationInstruction">
                    <h>Validation Code</h>
                </div>
                
                <asp:TextBox runat="server" id="ValidationCodeInput" type="number" placeholder="Enter your validation code"></asp:TextBox>
                <div>
                    <asp:Label ID="ValidationInstructionMessage" runat="server" 
                               Text ="A validation code has been sent to your email"
                               Visible="true" style="font-size: 20px"></asp:Label>
                    <asp:Label ID="CodeInvalidErrorMessage" runat="server" 
                               Text ="Invalid length of code. Please check your code again"
                               Visible="false" style="font-size:20px" ForeColor="red"></asp:Label>  
                </div>                              
                <div>
                    <asp:Button runat="server" ID="CodeSubmitBtn" Text="submit"
                        OnClick="CodeSubmitBtn_Click" OnClientClick="updateTimer(); return true;"/>
                </div>
                <div>
                    <asp:Button runat="server" ID="CodeResendBtn" Text="Resend" 
                        OnClick="CodeResendBtn_Click" OnClientClick="TimerReset(); return true;" />
                </div>          
                <div id="timer" runat="server"></div>
            </div>
        </div>
    </form>
</body>
</html>
