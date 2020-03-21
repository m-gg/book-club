/*
 * Filename: Create-Account.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Create-Account.aspx
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Online_Book_Club.BookClubRef;
using System.Web.UI.HtmlControls; // For delay before redirect

namespace Online_Book_Club
{
    public partial class Create_Account : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.

        protected void Page_Load(object sender, EventArgs e)
        {
            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
            BtnSubmit.Enabled = true;
            lblSuccess.Visible = false;

            // Redirect to home page if already logged in.
            if ((Request.Cookies["LoggedIn"] != null) && (Request.Cookies["LoggedIn"].Value == "true"))
            {
                Response.Redirect("~/");
            }
        }

         //  The custom validator checks if the user entered a valid password.
         //  I made the password requirements only a 4 character minimum length, with no extra requirements.
         //  This is obviously not secure, and stronger validation would be required for a real-life situation.
        protected void CustomValPass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Validates password during password creation.
            if (txtPassword.Text.Length < 4)
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }

        // The user clicks the submit button in order to create an account.
        // Several validation checks insure that the user has entered all required details properly.
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            // Creates account for user if all validation checks passed.
            if (Page.IsValid)
            {
                BtnSubmit.Enabled = false; // Submit button is disabled while account creating is in progress

                // User information is saved in the database. Account is created.
                bookClub = new BookClubWebServiceSoapClient();                
                bookClub.AddNewUser(connectionString, txtName.Text, txtSurname.Text, txtEmail.Text, txtUsername.Text, txtPassword.Text, DropDownSecurityQ.SelectedValue, txtSecurityAnswer.Text);

                // Show success label
                lblSuccess.Visible = true;

                // The code below is used to redirect to the login page after 5 seconds.
                HtmlMeta delaydRedirect = new HtmlMeta
                {
                    HttpEquiv = "Refresh",
                    Content = "5,url=Login"
                };
                this.Page.Controls.Add(delaydRedirect);
            }
        }

        // This custom-validator insures that the user enters a unique username.
        // The username is used in several cases across this project to uniquely identify the user.
        protected void UsernameUnique_ServerValidate(object source, ServerValidateEventArgs args)
        {
            bookClub = new BookClubWebServiceSoapClient();

            // Checks if username is unique.
            if (bookClub.UserNameUnique(connectionString, txtUsername.Text)) // Username unique.
            {
                args.IsValid = true;
            }
            else // Username taken (not unique).
            {
                args.IsValid = false;
            }
        }
    }
}