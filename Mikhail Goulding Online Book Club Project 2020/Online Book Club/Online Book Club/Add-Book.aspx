<%--/*
 * Filename: Add-Book.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This page allows the user to add a new book to the database.
 */--%>

<%@ Page Title="Add Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Add-Book.aspx.cs" Inherits="Online_Book_Club.Add_Book" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .bookAdded {
            color: rgb(70, 235, 52);
        }
    </style>
    <h3>Add Book</h3>
    <hr class="line" />
    <div>
        <table>
            <tr>
                <td>Name:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtBookName" placeholder="Enter the book's name" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqBookName" runat="server" ErrorMessage="Please enter the book's name" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookName"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ValidatorBookExists" runat="server" ControlToValidate="txtBookName" ErrorMessage="Book already exists!" ForeColor="Red" Display="Dynamic" OnServerValidate="ValidatorBookExists_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Author:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtBookAuthor" placeholder="Enter the author's name" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqBookAuthor" runat="server" ErrorMessage="Please enter the author's name" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookAuthor"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>ISBN:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtBookISBN" placeholder="Enter the book's ISBN number" runat="server" Width="370px" CssClass="textbox"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ReqBookISBN" runat="server" ErrorMessage="Please enter the book's ISBN number" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookISBN"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ValidatorISBNok" runat="server" ErrorMessage="ISNB number needs to be 10 digits long" ForeColor="Red" Display="Dynamic" OnServerValidate="ValidatorISBNok_ServerValidate" ControlToValidate="txtBookISBN"></asp:CustomValidator>
                    <asp:RegularExpressionValidator ID="BookISBNdigitsOnly" runat="server" ErrorMessage="Only numbers allowed in ISBN number." ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookISBN" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                    <asp:CustomValidator ID="ValidatorISBNexists" runat="server" ErrorMessage="Book with this ISBN number already exists!" ForeColor="Red" Display="Dynamic" OnServerValidate="ValidatorISBNexists_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td>Pages:</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="height: 24px">
                    <asp:TextBox ID="txtBookPages" placeholder="Enter the number of pages the book has" runat="server" Width="370px" CssClass="textbox" TextMode="Number"></asp:TextBox>
                </td>
                <td style="height: 24px">
                    <asp:RequiredFieldValidator ID="ReqBookPages" runat="server" ErrorMessage="Please enter the book's number of pages" ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookPages"></asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="ValidatorBookPages" runat="server" ErrorMessage="Book needs to have at least 1 page." ForeColor="Red" Display="Dynamic" ControlToValidate="txtBookPages" OnServerValidate="ValidatorBookPages_ServerValidate"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td style="padding-top: 12px;">
                    <asp:Button ID="BtnSubmitToAddBook" runat="server" Text="Submit" CssClass="button" OnClick="BtnSubmitToAddBook_Click" />
                </td>
                <td>
                    <asp:Label ID="lblBookSuccess" runat="server" Text="Success! You will be redirected to the Books page in 5 seconds." CssClass="bookAdded" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
