<%--/*
 * Filename: About.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The about page gives some general information on using the website.
 */--%>

<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Online_Book_Club.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h3>Welcome to Super &#9733; Book Club&trade;</h3>
        <div class="grey"><em>Also known as Super Star Book Club!</em></div>
        <br />
        The purpose of this website is to rate books.
        <br />
        Submits books that you have read and rate them! See how the book average changes as other users rate your book!
        <br />
        Rate books others have submitted!
        <br />
        Remember to sit in a well lit room while playing around on this site.
    </div>
</asp:Content>
