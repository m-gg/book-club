/*
 * Filename: Default.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Default.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Online_Book_Club
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Two sections hidden for now.
            LoginOnly.Visible = false;
            LogOutOnly.Visible = false;

            // Displays sections depending on whether or not the user has logged in.
            if ((Request.Cookies["LoggedIn"] != null) && (Request.Cookies["LoggedIn"].Value == "true") && (Request.Cookies["Username"] != null) && (Request.Cookies["Username"].Value != "")) // Logged in and username cookie has value.
            {
                // Decodes the url-encoded username cookie and html-encodes it before displaying it as label.
                string username = Server.HtmlEncode(Server.UrlDecode(Request.Cookies["Username"].Value));
                UserNameLabel.Text = " " + username.ToLower();

                LoginOnly.Visible = true; // Only shows when properly logged in
            }
            else // Not loggged in.
            {
                LogOutOnly.Visible = true; // Only shows when not logged in.
            }
        }
    }
}