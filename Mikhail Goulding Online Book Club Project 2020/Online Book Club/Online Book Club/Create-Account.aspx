<%--/*
 * Filename: Create-Account.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The user creates an account from this page.
 */--%>

<%@ Page Title="Create Account" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Create-Account.aspx.cs" Inherits="Online_Book_Club.Create_Account" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .info {
            color: rgb(179, 179, 179);
            display: inline;
        }

        .success {
            color: rgb(70, 235, 52);
        }
    </style>
    <h3>Create Account</h3>
    <hr class="line" />
    <div>
        <table>
            <tr>
                <td>Name:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtName" placeholder="Enter your name" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqName" runat="server" ControlToValidate="txtName" ErrorMessage="Please enter your name" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Surname:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSurname" placeholder="Enter your surname" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqSurname" runat="server" ControlToValidate="txtSurname" ErrorMessage="Please enter your surname" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Email Address:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtEmail" placeholder="Enter your email address" runat="server" Width="370px" CssClass="textbox" TextMode="Email"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Please enter your email address" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    &nbsp;<asp:RegularExpressionValidator ID="EmailValid" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Please enter a valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Username:
                    <div class="info">(Used to log in)</div>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtUsername" placeholder="Enter your username (Used to log in)" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqUsername" runat="server" ControlToValidate="txtUsername" ErrorMessage="Please enter a username" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="UsernameUnique" runat="server" ErrorMessage="Username taken! Please use a unique username" ForeColor="Red" Display="Dynamic" OnServerValidate="UsernameUnique_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Password:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtPassword" placeholder="Enter a password" runat="server" Width="370px" CssClass="textbox" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqPass" runat="server" ControlToValidate="txtPassword" ErrorMessage="Please enter a password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    &nbsp;<asp:CustomValidator ID="CustomValPass" runat="server" ErrorMessage="Please enter at least 4 characters" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword" OnServerValidate="CustomValPass_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Confirm Password:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtPasswordConfirm" placeholder="Enter password again" runat="server" Width="370px" CssClass="textbox" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqPass2" runat="server" ControlToValidate="txtPasswordConfirm" ErrorMessage="Please re-enter your password" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="ComparePass" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" Display="Dynamic" ErrorMessage="Passwords do not match!" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Security Question:
                    <div class="info">(For password recovery)</div>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownSecurityQ" runat="server" Width="370px" CssClass="textbox">
                        <asp:ListItem Value="Pet">What is your first pet&#39;s name?</asp:ListItem>
                        <asp:ListItem Value="Song">What is your favourite song?</asp:ListItem>
                        <asp:ListItem Value="Food">What is your favourite food?</asp:ListItem>
                        <asp:ListItem Value="Life">What is the meaning of life?</asp:ListItem>
                        <asp:ListItem Value="Crime">How many crimes have you committed?</asp:ListItem>
                        <asp:ListItem Value="Pizza">Why do you like pineapple on pizza?</asp:ListItem>
                        <asp:ListItem Value="Zombie">How old is your pet zombie?</asp:ListItem>
                        <asp:ListItem Value="Time">Who built the first time-machine?</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqSecurityQ" runat="server" ErrorMessage="Please choose one of the options" ForeColor="Red" Display="Dynamic" ControlToValidate="DropDownSecurityQ"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>Answer:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSecurityAnswer" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqSecurityAnswer" runat="server" ControlToValidate="txtSecurityAnswer" Display="Dynamic" ErrorMessage="Please answer the security question" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 12px;">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="BtnSubmit_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label ID="lblSuccess" runat="server" Text="Success! You will be redirected to the login page in 5 seconds." CssClass="success" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
