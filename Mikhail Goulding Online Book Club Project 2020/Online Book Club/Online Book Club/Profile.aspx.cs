/*
 * Filename: Profile.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Profile.aspx
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
    public partial class Profile : System.Web.UI.Page
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
            bookClub = new BookClubWebServiceSoapClient();

            BtnSubmit.Enabled = true;

            // The user's details are showed in the appropriate textboxes.
            if (!Page.IsPostBack)
            {
                string username = Server.UrlDecode(Request.Cookies["Username"].Value);

                List<string> detail = bookClub.UserDetails(connectionString, username); // User details retrieved and stored in a list.

                // User details displayed in textboxes. The user can now edit or view his  information.
                txtName.Text = detail[0];
                txtSurname.Text = detail[1];
                txtEmail.Text = detail[2];
            }
        }

        // User's personal details are updated when the submit button is clicked.
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Username cookie gets decoded.
            bookClub = new BookClubWebServiceSoapClient();
            string username = Server.UrlDecode(Request.Cookies["Username"].Value);

            // User personal details gets updated. 
            bookClub.UpdateUserDetail(connectionString, username, txtName.Text, txtSurname.Text, txtEmail.Text);
            lblSuccess.Visible = true; // User gets notified that details where changed successfully.
            BtnSubmit.Enabled = false; // Submit button temporarily disabled to avoid bugs.

            // The code below is used to redirect to the home page after 5 seconds.
            HtmlMeta delaydRedirect = new HtmlMeta
            {
                HttpEquiv = "Refresh",
                Content = "5,url=/"
            };
            this.Page.Controls.Add(delaydRedirect);
        }
    }
}