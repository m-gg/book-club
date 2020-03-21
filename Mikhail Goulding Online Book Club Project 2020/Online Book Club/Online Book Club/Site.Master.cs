/*
 * Filename: Site.Master.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file is part of the masterpage. Meaning, it will run for each page. It is mainly used to show or hide the login,
 * logout and create account buttons/links. This is done by checking if the user is logged in each time a page is loaded.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Book_Club
{
    public partial class SiteMaster : MasterPage
    {
        // The Page_Load event below triggers for all webpages in this project, since they all use this masterpage.
        protected void Page_Load(object sender, EventArgs e)
        {
            // Checks if user is logged in and hides/shows buttons and links accordingly.
            if ((Request.Cookies["LoggedIn"] != null) && (Request.Cookies["LoggedIn"].Value == "true")) // If logged in.
            {
                // Hides create account and login links.
                CreateAccountLink.Visible = false;
                LoginLink.Visible = false;

                // Shows log-out button and Books page link.
                LogOutBtn.Enabled = true;
                LogOutBtn.Visible = true;
                BooksLink.Visible = true;
            }
            else // If not logged in.
            {
                // Hides log-out button and Books page link.
                LogOutBtn.Enabled = false;
                LogOutBtn.Visible = false;
                BooksLink.Visible = false;
            }
        }

        // User gets logged out and is redirected to the login page when he clicks the logout button.
        protected void LogOutBtn_Click(object sender, EventArgs e)
        {
            // User cookies get emptied/reset, logging him out.
            HttpCookie loggedin = new HttpCookie("LoggedIn", "false");
            HttpCookie usernameCookie = new HttpCookie("Username", "");
            Response.Cookies.Add(loggedin);
            Response.Cookies.Add(usernameCookie);
            Response.Redirect("/Login");
        }
    }
}