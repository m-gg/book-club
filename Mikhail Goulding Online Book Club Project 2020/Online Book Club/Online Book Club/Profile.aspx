<%--/*
 * Filename: Profile.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The user can reset or view his personal details from this page.
 */--%>

<%@ Page Title="Change Account Details" Language="C#"  MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Online_Book_Club.Profile" ValidateRequest="false" %>

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
    <h3>Change Account Details:</h3>
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
                <td style="padding-top: 12px;">
                    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" CssClass="button" OnClick="BtnSubmit_Click" />
                    <br />
                </td>
                <td>
                    <asp:Label ID="lblSuccess" runat="server" Text="Success! Your account details have been changed. You will be redirected to the home page in 5 seconds." CssClass="success" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
