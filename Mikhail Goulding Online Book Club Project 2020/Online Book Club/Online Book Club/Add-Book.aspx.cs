/*
 * Filename: Add-Book.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Add-Book.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Online_Book_Club.BookClubRef;

namespace Online_Book_Club
{
    public partial class Add_Book : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.

        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirects to login page if not logged in.
            if ((Request.Cookies["LoggedIn"] == null) || (Request.Cookies["LoggedIn"].Value != "true"))
            {
                Response.Redirect("Login");
            }

            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
           
            BtnSubmitToAddBook.Enabled = true; // Submit book button (re)-enabled.
        }

        protected void ValidatorISBNok_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Makes sure that isbn number is 10 digits long.
            if (txtBookISBN.Text.Length == 10 ) // 10 digits long
            {
                args.IsValid = true;
            }
            else // Not 10 digits long.
            {
                args.IsValid = false;
            }
        }

        // Custom validator ensures that the book has a valid number of pages.
        protected void ValidatorBookPages_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Makes sure that the page number is at least 1 page long.
            if (int.Parse(txtBookPages.Text) >= 1) // Book page number valid.
            {
                args.IsValid = true;
            }
            else // Book page number not valid.
            {
                args.IsValid = false;
            }
        }

        // Custom validator ensures that the isbn number entered is unique.
        protected void ValidatorISBNexists_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bookClub = new BookClubWebServiceSoapClient();

            // check if book isbn is unique
            if (bookClub.BookISBNUnique(connectionString, txtBookISBN.Text)) // If unique
            {
                args.IsValid = true;
            }
            else // If book isbn already exists.
            {
                args.IsValid = false;
            }
        }

        // Custom validator ensures that the book name entered is unique.
        protected void ValidatorBookExists_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bookClub = new BookClubWebServiceSoapClient();

            // Check if book name is unique.
            if (bookClub.BookNameUnique(connectionString, txtBookName.Text)) // If unique
            {
                args.IsValid = true;
            }
            else // If book name already exists.
            {
                args.IsValid = false;
            }
        }

        // If the page is valid, the book gets added to the database.
        protected void BtnSubmitToAddBook_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                BtnSubmitToAddBook.Enabled = false;
                
                // Book information is saved in the database.
                bookClub = new BookClubWebServiceSoapClient();
                bookClub.AddNewBook(connectionString, txtBookName.Text, txtBookAuthor.Text, txtBookISBN.Text, int.Parse(txtBookPages.Text));

                lblBookSuccess.Visible = true; // Label notifies user that book was successfully added to the database.

                // The code below is used to redirect to the Books page after 5 seconds.
                HtmlMeta delaydRedirect = new HtmlMeta
                {
                    HttpEquiv = "Refresh",
                    Content = "5,url=Books"
                };
                this.Page.Controls.Add(delaydRedirect);
            }
        }
    }
}