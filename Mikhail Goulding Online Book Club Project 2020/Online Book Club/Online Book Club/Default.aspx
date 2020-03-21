<%--/*
 * Filename: Default.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This is the home page. The information on this page changes depending on whether or not the user is logged in.
 */--%>

<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Online_Book_Club._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .gif {
            margin-left: auto;
            margin-right: auto;
            text-align: center;
        }

        .username {
            color: rgb(217, 159, 13);
            text-shadow: 1px 1px black;
        }
    </style>
    <div class="gif">
        <h2>Welcome<asp:Label ID="UserNameLabel" runat="server" Text="" CssClass="username"></asp:Label>!
        </h2>
        <br />
        <div id="LoginOnly" runat="server">
            <div class="gif">
                ☆ <a href="Books">Start Rating books!</a> ☆<br />
                <br />
                <a href="Books">
                    <img src="gif/1.gif" style="max-height:80%; max-width:80%" />
                </a>
                <br />
                <br />
                ☆ <a href="Profile">Change Personal Information</a> ☆
                <br />
                ☆
                <br />
                ☆ <a href="Reset">Change Password</a> ☆<br />
            </div>
        </div>
        <div id="LogOutOnly" runat="server">
            Please <a href="Login">log in</a> or <a href="Create-Account">create an account.</a>
        </div>
    </div>
</asp:Content>
