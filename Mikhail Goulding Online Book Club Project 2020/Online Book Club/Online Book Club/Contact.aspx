<%--/*
 * Filename: Contact.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The contact page lists the contact information for the company. Please note that this information is fake.
 */--%>

<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Online_Book_Club.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Contact Us:</h3>

        <address>
        <strong>Email:</strong>   <a href="mailto:starbooks@sblimited.co">starbooks@sblimited.co</a><br />
        <strong>Phone:</strong> <a href="tel:+27555647577">055 564 7577</a> <br />
        <strong>Fax:</strong> <a href="fax:+27866555321">086 655 5321</a>
    </address>

    <h4>Physical Address:</h4>
    <strong>Star Books Limited</strong>
    <address>
        1483 Protea St<br />
        Greenwood Hill, Port Elizabeth<br />
        Eastern Cape, South Africa<br />
        6005
    </address>

</asp:Content>
