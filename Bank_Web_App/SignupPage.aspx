<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignupPage.aspx.cs" Inherits="BankWebApp.SigninPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="content-type" content="text/html" charset="utf-8" />
    <link rel="StyleSheet" href="./css/SignupPageStyle.css"/>
    <link href="https://fonts.cdnfonts.com/css/multicolore-2" rel="stylesheet">
    <title>Sign Up Page</title>
    <style>
        @import url('https://fonts.cdnfonts.com/css/multicolore-2');
    </style>
                
    <script type="text/javascript">
        function togglePasswordVisibility(inputId, eyeIcon) {
            var passwordInput = document.getElementById(inputId);
            if (passwordInput.type === "password") {
                passwordInput.type = "text";
                eyeIcon.src = "./css/eyeClose.png";
                eyeIcon.alt = "Hidden Eye";
            } else {
                passwordInput.type = "password";
                eyeIcon.src = "./css/eyeOpen.png";
                eyeIcon.alt = "Visible Eye";
            }
        }
    </script>

</head>
<body>
    <form id="LoginPageForm" runat="server">
        <div id="SignupFramework">
            <img id="LogoImage" src="./css/Logo.png"/><br />
            <div id="SignupInput">
                <label for="txtEmail">E-mail</label><br />
                <input type="text" id="txtEmail" placeholder="E-mail" /><br />
        
                <label for="txtPassword">Password</label><br />              
                <div class="password-container">
                    <input type="password" id="txtPassword" placeholder="Password" />
                    <img class="toggle-password" src="./css/eyeOpen.png" alt="Visible Eye" onclick="togglePasswordVisibility('txtPassword', this)" />
                </div>
                        
                <label for="txtRePassword">Re-enter Password</label><br />
                <div class="password-container">
                    <input type="password" id="txtRePassword" placeholder="Re-enter Password" />
                    <img class="toggle-password" src="./css/eyeOpen.png" alt="Visible Eye" onclick="togglePasswordVisibility('txtRePassword', this)" />
                </div>
                
                <label for="txtFirstName">First Name</label><br />
                <input type="text" id="txtFirstName" placeholder="First Name" /><br />
                
                <label for="txtLastName">Last Name</label><br />
                <input type="text" id="txtLastName" placeholder="Last Name" /><br />
                
                <label for="txtDateOfBirth">Date Of Birth</label><br />
                <input type="date" id="txtDateOfBirth" placeholder="Date Of Birth" /><br />
                

                <label for="txtProvince">Province</label><br />
                <select id="dlProvince">
                  <option value="Alberta">Alberta</option>
                  <option value="British Columbia">British Columbia</option>
                  <option value="Manitoba">Manitoba</option>
                  <option value="New Brunswick">New Brunswick</option>
                  <option value="Newfoundland and Labrador">Newfoundland and Labrador</option>
                  <option value="Nova Scotia">Nova Scotia</option>
                  <option value="Ontario">Ontario</option>
                  <option value="Prince Edward Island">Prince Edward Island</option>
                  <option value="Quebec">Quebec</option>
                  <option value="Saskatchewan">Saskatchewan</option>
                  <option value="Northwest Territories">Northwest Territories</option>
                  <option value="Nunavut">Nunavut</option>
                  <option value="Yukon">Yukon</option>
                </select><br />

                <label for="txtCity">City</label><br />
                <input type="text" id="txtCity" placeholder="City" /><br />
                
                <label for="txtAddress">Address</label><br />
                <input type="text" id="txtAddress" placeholder="Address" /><br />
                
                <label for="txtPostal">Postal Code</label><br />
                <input type="text" id="txtPostal" placeholder="Postal Code" /><br />
                
                
                
                
                
                
                
                <label for="txtPhoneNumber">Phone Number</label><br />
                <input type="tel" id="txtPhoneNumber" placeholder="Phone Number" /><br />                     
                
                <input type="submit" id="btnSignup" value="Sign Up" onclick="btnSignup_Click"/>

            </div>
        </div>
    </form>
</body>
</html>
