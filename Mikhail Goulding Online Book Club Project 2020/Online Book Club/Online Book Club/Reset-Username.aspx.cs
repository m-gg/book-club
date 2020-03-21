/*
 * Filename: Reset-Username.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Reset-Username.aspx
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
    public partial class Reset_Username : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.

        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
        }

        // When the user clicks the submit button, it checks if the page is valid.
        // The code inside will only execute if the username exists. (meaning, if the page is valid)
        protected void ResetUsernameSubmitBtn_Click(object sender, EventArgs e)
        {
            // Checks if page is valid and redirects to the Reset.aspx page if it is.
            if (Page.IsValid)
            {
                Response.Redirect("/Reset");
            }
        }

        // Custom validator checking if the username exists. The username gets stored as a cookie if it does exist.
        protected void UsernameExistValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bookClub = new BookClubWebServiceSoapClient();

            // Checks if username exists.
            if (!(bookClub.UserNameUnique(connectionString, UsernameRtxt.Text))) // Username exists
            {
                // Username stored as a cookie.
                HttpCookie usernameCookie = new HttpCookie("Username",Server.UrlEncode(UsernameRtxt.Text));
                Response.Cookies.Add(usernameCookie);

                args.IsValid = true;
            }
            else // Username does not exist.
            {
                args.IsValid = false;
            }
        }
    }
}