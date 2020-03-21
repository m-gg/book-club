<%--/*
 * Filename: Reset.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The user can reset his password from here. A security question needs to be answered before changing the password.
    Once the password is changed, the user is logged out (if logged in), and sent to the login page.
 */--%>

<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reset.aspx.cs" Inherits="Online_Book_Club.Reset" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .success {
            color: rgb(70, 235, 52);
        }
    </style>
    <h3>Reset Password</h3>
    <hr class="line" />
    <table>
        <tr>
            <td>Security Question:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="SecurityQTxtBox" runat="server" ReadOnly="True" Width="370px" CssClass="textbox"></asp:TextBox>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>Answer:</td>
            <td>
            &nbsp;
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="SecurityAnswerTxtbox" placeholder="Enter answer to security question" runat="server" Width="370px" CssClass="textbox"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="ReqSecurityQuestion" runat="server" ErrorMessage="Please enter an answer" ForeColor="Red" Display="Dynamic" ControlToValidate="SecurityAnswerTxtbox"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="SecurityQValidator" runat="server" ErrorMessage="Answer incorrect!" ForeColor="Red" Display="Dynamic" ControlToValidate="SecurityAnswerTxtbox" OnServerValidate="SecurityQValidator_ServerValidate"></asp:CustomValidator></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>New Password:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPasswordReset" placeholder="Enter a new password" runat="server" Width="370px" CssClass="textbox" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ReqPass" runat="server" ErrorMessage="Please enter a new password" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPasswordReset"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="CustomValidatorPass" runat="server" ErrorMessage="Please enter at least 4 characters" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPasswordReset" OnServerValidate="CustomValidatorPass_ServerValidate"></asp:CustomValidator>

            </td>
        </tr>
        <tr>
            <td>Confirm New Password:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPasswordConfirmReset" placeholder="Enter password again" runat="server" Width="370px" CssClass="textbox" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ReqPassConfirm" runat="server" ErrorMessage="Please enter your new password again" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPasswordConfirmReset"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareNewPass" runat="server" Display="Dynamic" ErrorMessage="Passwords do not match!" ForeColor="Red" ControlToCompare="txtPasswordReset" ControlToValidate="txtPasswordConfirmReset"></asp:CompareValidator>

            </td>
        </tr>
        <tr>
            <td style="padding-top: 12px;">
                <asp:Button ID="ButtonChangePass" runat="server" Text="Change Password" CssClass="button" OnClick="ButtonChangePass_Click" />
            </td>
            <td>
                <asp:Label ID="lblPassChanged" runat="server" Text="Success! You will be redirected to the login page in 5 seconds." CssClass="success" Visible="False"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
