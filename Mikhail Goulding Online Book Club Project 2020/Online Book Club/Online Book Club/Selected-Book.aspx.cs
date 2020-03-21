/*
 * Filename: Selected-Book.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Selected-Book.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online_Book_Club.BookClubRef;
using System.IO;

namespace Online_Book_Club
{
    public partial class Selected_Book : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.

        protected void Page_Load(object sender, EventArgs e)
        {
            #region Redirect
            // Redirects to login page if not logged in.
            if ((Request.Cookies["LoggedIn"] == null) || (Request.Cookies["LoggedIn"].Value != "true"))
            {
                Response.Redirect("Login");
            }

            // Redirects to Books page if SelectedBook cookie does not exist.
            if (Request.Cookies["SelectedBook"] == null || Request.Cookies["SelectedBook"].Value == "")
            {
                Response.Redirect("Books");
            }
            #endregion

            #region BookDetails
            //The code in the BookDetails region displays the information about the selected book inside a gridview.

            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
            bookClub = new BookClubWebServiceSoapClient();

            string bookname = Server.UrlDecode(Request.Cookies["SelectedBook"].Value); // Retrieves and decodes selected book name from the cookie.

            BookClubRef.Book book = bookClub.BookInfo(connectionString, bookname); // Retrieves all information about the book by requesting the web service.

            // New instance of the Book class is created, used to store properties containing the book's details.
            Book convertedBook = new Book
            {
                Name = book.name,
                Author = book.author,
                ISBN = book.ISBN,
                Pages = book.pages
            };

            // New list is created to store the book details, and the "converted" book is added to it.
            // All of this is done so the information can be displayed through the gridview.
            List<Book> bookToBind = new List<Book>();
            bookToBind.Add(convertedBook);
            

            // Book is bound to the gridview and displayed.
            BookInfoGridView.DataSource = bookToBind;
            BookInfoGridView.DataBind();
            #endregion

            #region Rating
            // This region displays the average rating for the book and allows the user to rate the book.

            // Hides/shows some sections.
            RateBook.Visible = false;
            UserRating.Visible = true;


            // Get username and book name in order to check if user has rated the book.
            string cookieusername = Server.UrlDecode(Request.Cookies["Username"].Value); // Username retrieved from cookie and decoded.
            string cookiebookname = Server.UrlDecode(Request.Cookies["SelectedBook"].Value); // Selected book name retrieved from cookie and decoded.
            int rating = bookClub.UserRated(connectionString, cookieusername, cookiebookname); // User rating is returned. 0 is returned if the user has not rated the book.

            // Shows the average rating for the book.
            double average = Math.Round(bookClub.AverageRating(connectionString, cookiebookname), 1); // Average rating for book is rounded.
            AverageRatingLbl.Text = average.ToString(); // Rounded rating is displayed inside a label.


            // Checks if user has rated the book, and shows the user rating as stars (☆).
            // As said above, the user rating will be 0 if he has not rated the book.
            switch (rating)
            {
                case 1:
                    {
                        ratingLabel.Text = "☆";
                        break;
                    }
                case 2:
                    {
                        ratingLabel.Text = "☆ ☆";
                        break;
                    }
                case 3:
                    {
                        ratingLabel.Text = "☆ ☆ ☆";
                        break;
                    }
                case 4:
                    {
                        ratingLabel.Text = "☆ ☆ ☆ ☆";
                        break;
                    }
                case 5:
                    {
                        ratingLabel.Text = "☆ ☆ ☆ ☆ ☆";
                        break;
                    }
                default: // User has not rated the book.
                    {
                        // Shows the book rating section if the user has not rated the book yet.
                        // This section allows the user to rate the book.
                        RateBook.Visible = true;
                        UserRating.Visible = false;
                        break;
                    }
            }
            #endregion
        }

        // Submit button for when the user rates the book.
        // Here, the rating gets saved, and is logged in a textfile. The logfile can be found inside the default program directory.
        // The page is also refreshed in order to show the user's rating.
        protected void ScoreSubmitButton_Click(object sender, EventArgs e)
        {         
            // Username, book name cookies are decoded and stored inside variables. User's selected rating is stored inside a variable.
            string username = Server.UrlDecode(Request.Cookies["Username"].Value);
            string bookname = Server.UrlDecode(Request.Cookies["SelectedBook"].Value);
            int rating = int.Parse(UserRatingRBList.SelectedValue);

            // The user rating is stored in the database.
            bookClub = new BookClubWebServiceSoapClient();
            bookClub.AddRating(connectionString, username, bookname, rating);

            #region ToLog
            // Adds user rating information to a log file.
            // Log File Location: Online Book Club\Online Book Club\ratinglog.txt

            string logText = "Username: " + username + ", Book: " + bookname + ", Rating: " + rating + "/5"; // Text for log file.
            string logPath = Request.PhysicalApplicationPath + "/ratinglog.txt"; // Log file location dynamically set.

            // The information is stored in the log file on a new line. (Without overwriting old logs)
            StreamWriter sw = new StreamWriter(logPath, true);
            sw.WriteLine(logText);
            sw.Close();
            #endregion

            // Refresh page:
            Response.Redirect("Selected-Book");
        }
    }

    // This class stores information (properties) about the book. Used for easy data-binding to gridview.
    public class Book
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
        private string author = ""; // Book author
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
            }
        }
        private string isbn = ""; // Book isbn number
        public string ISBN
        {
            get
            {
                return isbn;
            }
            set
            {
                isbn = value;
            }
        }

        private int pages = 1; // Number of pages the book has.
        public int Pages
        {
            get
            {
                return pages;
            }
            set
            {
                pages = value;
            }
        }
    }
}