/*
 * Filename: Books.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Books.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online_Book_Club.BookClubRef;

namespace Online_Book_Club
{
    public partial class Books : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirects to login page if not logged in.
            if ((Request.Cookies["LoggedIn"] == null) || (Request.Cookies["LoggedIn"].Value != "true"))
            {
                Response.Redirect("Login");
            }
            
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
            BookClubWebServiceSoapClient bookClub = new BookClubWebServiceSoapClient();

            List<string> returnedBooks = bookClub.BookList(connectionString); // Stores a list of all books in the database.
            List<BookName> booklist = new List<BookName>(); // The names of all books in the database will be stored in this list as properties.

            // Loops through all books returned and stores their names in a seperate list as properties.
            // This is done in order to easily display the book names inside a gridview.
            foreach (string recievedBook in returnedBooks)
            {
                booklist.Add(new BookName { Name = recievedBook });
            }

            // The book names are now data-bound, and displayed inside the gridview.
            BooksGridview.DataSource = booklist;
            BooksGridview.DataBind();
        }

        // Class stores the name of a book as a property. The property is required to make databinding with the gridview work properly.
        public class BookName
        {
            private string name = ""; // Book name
            public string Name
            {
                get
                {
                    return name;
                }
                set
                {
                    name = value;
                }
            }
        }

        // When the user selects a book, the book's name is stored inside a cookie.
        protected void BooksGridview_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = BooksGridview.SelectedRow.Cells[1].Text; // Book name retrieved from gridview.

            // Book name is encoded and stored as a cookie. 
            // User is also redirected to the Selected-Book.aspx page, where he can view information on the book and rate it.
            HttpCookie selectedBook = new HttpCookie("SelectedBook", Server.UrlEncode(name));
            Response.Cookies.Add(selectedBook);
            Response.Redirect("Selected-Book");
        }

        // I use this event in order to add some spacing for the book-name rows in the gridview.
        protected void BooksGridview_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Adds extra width to the gridview to make it look better.
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Width = new Unit("600px");
            }
        }
    }
}