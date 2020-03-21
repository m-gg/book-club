<%--/*
 * Filename:Login.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The user logs in from this page by providing his username and password.
 */--%>

<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Online_Book_Club.Login" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .link {
            color: rgb(153, 153, 153);
        }
    </style>
    <h3>Login</h3>
    <hr class="line" />
    <table>
        <tr>
            <td>Username:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtUsername" placeholder="Enter your username" runat="server" Width="370px" CssClass="textbox"></asp:TextBox></td>
            <td>
                <asp:RequiredFieldValidator ID="ReqUsername" runat="server" ErrorMessage="Please enter your username" ForeColor="Red" Display="Dynamic" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="UsernameExists" runat="server" ErrorMessage="Username does not exist!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtUsername" OnServerValidate="UsernameExists_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td>Password:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtPassword" placeholder="Enter your password" runat="server" Width="370px" CssClass="textbox" TextMode="Password"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ReqPassword" runat="server" ErrorMessage="Please enter your password" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="PasswordValid" runat="server" ErrorMessage="Password incorrect!" ForeColor="Red" Display="Dynamic" ControlToValidate="txtPassword" OnServerValidate="PasswordValid_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 12px;">
                <asp:Button ID="BtnLogin" runat="server" Text="Login" CssClass="button" OnClick="BtnLogin_Click" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <a href="Reset-Username" class="link">Forgot your password? Click here to reset</a>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
