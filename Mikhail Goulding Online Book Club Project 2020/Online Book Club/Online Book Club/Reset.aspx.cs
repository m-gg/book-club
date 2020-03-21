/*
 * Filename: Reset.aspx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This code-behind file of Reset.aspx
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
    public partial class Reset : System.Web.UI.Page
    {
        string connectionString; // Connection string for connecting to the database.
        BookClubWebServiceSoapClient bookClub; // Used for communicating with the web service.
        string username;

        // This collection stores the password-recovery security questions for easy access.
        public static Dictionary<string, string> securityQuestion = new Dictionary<string, string>
        {
            {"Pet", "What is your first pet's name?"},
            {"Song", "What is your favourite song?"},
            {"Food", "What is your favourite food?"},
            {"Life", "What is the meaning of life?"},
            {"Crime", "How many crimes have you committed?"},
            {"Pizza", "Why do you like pineapple on pizza?" },
            {"Zombie", "How old is your pet zombie?"},
            {"Time", "Who built the first time-machine?"}
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            lblPassChanged.Visible = false;
            ButtonChangePass.Enabled = true;

            connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Request.PhysicalApplicationPath + @"\BookClub.accdb";
            bookClub = new BookClubWebServiceSoapClient();

            if (Request.Cookies["Username"] != null) // If the username cookie exists.
            {
                username = Server.UrlDecode(Request.Cookies["Username"].Value);
                string question = bookClub.SecurityQuestion(connectionString, username); // Finds the security-question assosiated with the username.

                // Simle check to ensure that a value was returned.
                if (question != "") // If the security question was properly returned.
                {
                    SecurityQTxtBox.Text = securityQuestion[question].ToString();
                }
                else
                {
                    Response.Redirect("/Reset-Username");
                }
            }
            else // If the username cookie does not exist.
            {
                Response.Redirect("/Reset-Username"); // Redirects to start page of password reset if cookie is missing.
            }
        }

        // Custom validator checking if the security question was answered correctly.
        protected void SecurityQValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Checks if security question is answered correctly.
            if (bookClub.AnswerSecurityQuestionAttempt(connectionString, username, SecurityAnswerTxtbox.Text)) // If answered correctly.
            {
                args.IsValid = true;
            }
            else // If incorrect.
            {
                args.IsValid = false;
            }
        }

        // Change password button click. Password is only changed if page is valid.
        // Meaning, the passwords match, and the security question is answered correctly.
        protected void ButtonChangePass_Click(object sender, EventArgs e)
        {
            // Checks if the page is valid and changes the password if it is.
            if (Page.IsValid)
            {
                ButtonChangePass.Enabled = false; // Button temporarily disabled to avoid bugs.

                bookClub.ChangePassword(connectionString, username, txtPasswordReset.Text); // Password gets changed.

                // The user gets logged out if he was logged in while changing his password.
                HttpCookie loggedin = new HttpCookie("LoggedIn", "false");
                HttpCookie usernameCookie = new HttpCookie("Username", "");
                Response.Cookies.Add(loggedin);
                Response.Cookies.Add(usernameCookie);

                lblPassChanged.Visible = true; // Label notifies user that the password has changed successfully.

                // The code below is used to redirect to the login page after 5 seconds.
                HtmlMeta delaydRedirect = new HtmlMeta
                {
                    HttpEquiv = "Refresh",
                    Content = "5,url=Login"
                };
                this.Page.Controls.Add(delaydRedirect);
            }
        }

        // Custom validator ensures that a valid password is entered.
        protected void CustomValidatorPass_ServerValidate(object source, ServerValidateEventArgs args)
        {
            // Validates password during password creation. 
            // (As mentioned in create-account.aspx.cs, real life scenarios would require stronger validation)
            if (txtPasswordReset.Text.Length < 4) // Password invalid
            {
                args.IsValid = false;
            }
            else // Password is valid
            {
                args.IsValid = true;
            }
        }
    }
}