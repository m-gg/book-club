/*
 * Filename: Login.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Login.aspx
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
    public partial class Login : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.

        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";

            // Redirect to home page if already logged in.
            if ((Request.Cookies["LoggedIn"] != null) && (Request.Cookies["LoggedIn"].Value == "true"))
            {
                Response.Redirect("~/");
            }
        }

        // Custom validator checks if the entered username exists.
        protected void UsernameExists_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bookClub = new BookClubWebServiceSoapClient();

            // Checks if username exists
            if (!(bookClub.UserNameUnique(connectionString, txtUsername.Text)))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }

        // The user gets logged in if the page is valid, meaning if the username exists, and the username and password match.
        // These are checked in seperate custom-validators.
        protected void BtnLogin_Click(object sender, EventArgs e)
        {
            // User gets logged in if the page is valid.
            if (Page.IsValid)
            {
                HttpCookie loggedin = new HttpCookie("LoggedIn", "true"); // Logged in cookie.
                HttpCookie usernameCookie = new HttpCookie("Username", Server.UrlEncode(txtUsername.Text))
                {
                    Expires = loggedin.Expires = DateTime.Now.AddDays(7) // Sets expiry date for both cookies.
                }; // Save username in cookie

                Response.Cookies.Add(loggedin);
                Response.Cookies.Add(usernameCookie);
                Response.Redirect("~/");
            }
        }

        // Custom validator checks if the password entered is correct.
        protected void PasswordValid_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // The password entered will only be checked if the username exists.
            if (UsernameExists.IsValid)
            {
                bookClub = new BookClubWebServiceSoapClient();

                // Check if account details are correct. 
                if (bookClub.Login(connectionString, txtUsername.Text, txtPassword.Text)) // Password correct.
                {
                    args.IsValid = true;
                }
                else // Password incorrect.
                {
                    args.IsValid = false;
                }
            }
        }
    }
}