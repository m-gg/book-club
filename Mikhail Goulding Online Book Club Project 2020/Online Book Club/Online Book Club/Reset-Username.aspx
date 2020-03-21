<%--/*
 * Filename: Reset-Username.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: When attempting to reset your password while not logged in, you need to enter your username from this page.
    Once an existing username has been entered, it will redirect to the Reset.aspx page.
 */--%>

<%@ Page Title="Reset Password - Enter Username" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reset-Username.aspx.cs" Inherits="Online_Book_Club.Reset_Username" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Enter Username (for password reset)</h3>
    <hr class="line" />
    <table>
        <tr>
            <td>Username:</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="UsernameRtxt" runat="server" placeholder="Enter your username" Width="370px" CssClass="textbox"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="ReqUname" runat="server" ErrorMessage="Please enter your username" ForeColor="Red" Display="Dynamic" ControlToValidate="UsernameRtxt"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="UsernameExistValidator" runat="server" ErrorMessage="Username does not exist!" ForeColor="Red" Display="Dynamic" ControlToValidate="UsernameRtxt" OnServerValidate="UsernameExistValidator_ServerValidate"></asp:CustomValidator></td>
        </tr>
        <tr>
            <td style="padding-top: 12px;">
                <asp:Button ID="ResetUsernameSubmitBtn" runat="server" Text="Submit" CssClass="button" OnClick="ResetUsernameSubmitBtn_Click" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
