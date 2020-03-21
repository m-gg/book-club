<%--/*
 * Filename: Selected-Book.aspx
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: On this page the user can view information about a selected book. (A cookie is read in order to determine which book was read.)
    The user can see the average rating for the book, and can also rate the book.
    If the user has rated the book, he can see his rating when viewing this page.
 */--%>

<%@ Page Title="Selected Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Selected-Book.aspx.cs" Inherits="Online_Book_Club.Selected_Book" ValidateRequest="false" %>

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

        .centerme {
            text-align: center;
        }

        .space {
            margin: 4px;
            min-width: 20%;
            color: rgb(230, 230, 0);
        }

        .warning {
            color: red;
        }

        .centerTable {
            margin-left: auto;
            margin-right: auto;
            border: 1px dashed yellow;
        }

        .userRating {
            color: rgb(230, 230, 0);
        }
    </style>
    <hr />
    <div>
        <table class="centerTable">
            <tr>
                <td>Average Rating:
                    <asp:Label ID="AverageRatingLbl" runat="server" Text="0"></asp:Label>/5
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div>
        <asp:GridView ID="BookInfoGridView" runat="server" CssClass="mydatagrid">
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
    <div class="centerme"><a href="Books" class="grey">Return to book list</a></div>
    <div id="RateBook" runat="server">
        <br />
        <table class="centerTable">
            <tr>
                <td>
                    <h2>Rate the book:</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="UserRatingRBList" runat="server" CssClass="space">
                        <asp:ListItem Value="5" Selected="True">&nbsp; ☆ ☆ ☆ ☆ ☆</asp:ListItem>
                        <asp:ListItem Value="4">&nbsp; ☆ ☆ ☆ ☆</asp:ListItem>
                        <asp:ListItem Value="3">&nbsp; ☆ ☆ ☆</asp:ListItem>
                        <asp:ListItem Value="2">&nbsp; ☆ ☆</asp:ListItem>
                        <asp:ListItem Value="1">&nbsp; ☆</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ScoreSubmitButton" runat="server" Text="Submit" CssClass="button" OnClick="ScoreSubmitButton_Click" /></td>
            </tr>
            <tr>
                <td>
                    <div class="warning">Warning: Once you click submit, you cannot change your rating!</div>
                </td>
            </tr>
        </table>
    </div>
    <div id="UserRating" runat="server">
        <br />
        <table class=" centerTable">
            <tr>
                <td>Your rating:</td>
                <td>
                    <asp:Label ID="ratingLabel" runat="server" CssClass="userRating"></asp:Label></td>
            </tr>
        </table>
    </div>
</asp:Content>
