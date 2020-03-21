<%--/*
 * Filename: Books.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The books page displays a list of all books in the database. 
    If a user selects a book, he will be redirected to the Selected-Book.aspx page. A cookie saves the selected book's name.
    The user can choose to submit a new book, in which case he will be redirected to the Add-Book page.
 */--%>

<%@ Page Title="Books" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Books.aspx.cs" Inherits="Online_Book_Club.Books" ValidateRequest="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .mydatagrid {
            border: solid 2px grey;
            min-width: 20%;
            margin-left: auto;
            margin-right: auto;
        }

            .mydatagrid a {
                color: #fff;
                font-weight: bold;
            }

                .mydatagrid a:hover {
                    background-color: deepskyblue;
                }

            .mydatagrid td {
                padding: 5px;
            }

            .mydatagrid th {
                padding: 5px;
            }
    </style>
    <br />
    Select a book below to view it's information or rate it.
    <br />
    Don't see the book that you're looking for? <a href ="Add-Book" >Submit a new book here!</a>
    <hr />
    <div>
        <asp:GridView ID="BooksGridview" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" OnSelectedIndexChanged="BooksGridview_SelectedIndexChanged" CssClass="mydatagrid" OnRowDataBound="BooksGridview_RowDataBound">
            <Columns>
                <asp:BoundField HeaderText="Books" DataField="Name" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
</asp:Content>
