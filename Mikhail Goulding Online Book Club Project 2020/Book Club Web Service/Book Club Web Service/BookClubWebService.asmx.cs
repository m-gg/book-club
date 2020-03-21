/*
 * Filename: BookClubWebService.asmx.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: The web service is used to interact with the database. The "Online Book Club" project uses this web service.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.OleDb;

namespace Book_Club_Web_Service
{
    /// <summary>
    /// Summary description for BookClubWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class BookClubWebService : System.Web.Services.WebService
    {
        #region localMethods
        OleDbConnection conn; // Used for database connection

        // Connect to database.
        void Connect(string connectionString)
        {
            conn = new OleDbConnection(connectionString);
            conn.Open();
        }

        // Disconnect from database.
        void Disconnect()
        {
            conn.Close();
        }
        #endregion

        #region WebMethods
        // Add a new user to the database. Used during account creation.
        [WebMethod]
        public void AddNewUser(string connectionString, string name, string surname, string email, string username, string password, string question, string answer)
        {
            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Users (uName, uSurname, uEmail, uUsername, uPassword, uQuestion, uAnswer) VALUES (@name, @surname, @email, @username, @password, @question, @answer)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@question", question);
            command.Parameters.AddWithValue("@answer", answer);

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        // Checks if username is unique for account creation. Can also be used to check if username exists.
        [WebMethod]
        public bool UserNameUnique(string connectionString, string username)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE uUsername = @username", conn);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Checks username and password when user attempts to log in.
        [WebMethod]
        public bool Login(string connectionString, string username, string password)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE uUsername = @username AND uPassword = @password", conn);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read()) // If username/password combination is found. (Not case sensitive)
                {
                    // Check if passwords match (Case sensitive)
                    if (reader["uPassword"].ToString() == password) // Passwords match.
                    {
                        return true;
                    }
                    else // Passwords do not match.
                    {
                        return false;
                    }
                }
                else // Username/password incorrect.
                {
                    return false;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Returns user's chosen security question used when attempting to reset password.
        [WebMethod]
        public string SecurityQuestion(string connectionString, string username)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE uUsername = @username", conn);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();

                // Returns the user's security question if data on the user exists.
                if (reader.Read())
                {
                    return reader["uQuestion"].ToString(); // Returns the security question the user chose when creating an account.
                }
                else
                {
                    return "";
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Checks user's answer to security question.
        [WebMethod]
        public bool AnswerSecurityQuestionAttempt(string connectionString, string username, string answer)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Users WHERE uUsername = @username AND uAnswer = @answer", conn);
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@answer", answer);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read()) // True if value exists. Meaning, if the security question of the unique username was answered correctly.
                {
                    return true;
                }
                else // If the answer was incorrect.
                {
                    return false;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Changes the user's password
        [WebMethod]
        public void ChangePassword(string connectionString, string username, string password)
        {
            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "UPDATE Users SET uPassword = @password WHERE uUsername = @username";
            command.Parameters.AddWithValue("@password", password);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                command.ExecuteNonQuery(); // Passowrd gets changed.
            }
            finally
            {
                Disconnect();
            }
        }

        // Returns a list of all the book names. (Note: Book names are unique and used to identify the books)
        [WebMethod]
        public List<string> BookList(string connectionString)
        {
            List<string> books = new List<string>();

            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Books";

            try
            {
                OleDbDataReader reader = command.ExecuteReader();

                while (reader.Read()) // Adds class containing the book's name for, all books, to the list.
                {
                    string name = reader["bName"].ToString();

                    books.Add(name);
                }
                return books; // List containing a list of all book names returned.
            }
            finally
            {
                Disconnect();
            }
        }

        // Returns the details of a specific book.
        [WebMethod]
        public Book BookInfo(string connectionString, string bookName)
        {
            Book selectedBook = new Book();

            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Books WHERE bName = @bookname";
            command.Parameters.AddWithValue("@bookname",bookName);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();

                // The book's details are saved in a class and returned.
                selectedBook.name = reader["bName"].ToString();
                selectedBook.author = reader["bAuthor"].ToString();
                selectedBook.ISBN = reader["bISBN"].ToString();
                selectedBook.pages = (int)reader["bPages"];

                return selectedBook;
            }
            finally
            {
                Disconnect();
            }
        }

        // Checks if book name is unique for submitting new books. Can also be used to check if book exists.
        [WebMethod]
        public bool BookNameUnique(string connectionString, string bookname)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Books WHERE bName = @bookname", conn);
            command.Parameters.AddWithValue("@bookname", bookname);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read()) // If information is found, the name is not unique.
                {
                    return false;
                }
                else // Book name is unique.
                {
                    return true;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Checks if book isbn is unique for submitting new books. Can also be used to check if book exists.
        [WebMethod]
        public bool BookISBNUnique(string connectionString, string bookisbn)
        {
            Connect(connectionString);
            OleDbCommand command = new OleDbCommand("SELECT * FROM Books WHERE bISBN = @isbn", conn);
            command.Parameters.AddWithValue("@isbn", bookisbn);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                if (reader.Read()) // If information is found, the isbn number is not unique.
                {
                    return false;
                }
                else // ISBN number is unique.
                {
                    return true;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Adds a new book to the database.
        [WebMethod]
        public void AddNewBook(string connectionString, string name, string author, string isbn, int pages)
        {
            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "INSERT INTO Books (bName, bAuthor, bISBN, bPages) VALUES (@name, @author, @isbn, @pages)";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@author", author);
            command.Parameters.AddWithValue("@isbn", isbn);
            command.Parameters.AddWithValue("@pages", pages);

            try
            {
                command.ExecuteNonQuery(); // New book added to database.
            }
            finally
            {
                Disconnect();
            }
        }

        // Add a user's book rating to the database.
        [WebMethod]
        public void AddRating(string connectionString, string username, string bookname, int rating)
        {
            Connect(connectionString);

            // Command to get book id
            OleDbCommand commandBookName = conn.CreateCommand();
            commandBookName.CommandText = "SELECT * from Books WHERE bName = @name";
            commandBookName.Parameters.AddWithValue("@name", bookname);

            // Command to get user id
            OleDbCommand commandUserName = conn.CreateCommand();
            commandUserName.CommandText = "SELECT * from Users WHERE uUserName = @name";
            commandUserName.Parameters.AddWithValue("@name", username);
            
            // These two variables are used when storing the user's rating to the database.
            int userID; // Userid gets stored here. 
            int bookID; // Bookid gets stored here

            try
            {
                OleDbDataReader bookIdReader = commandBookName.ExecuteReader();
                OleDbDataReader userIdReader = commandUserName.ExecuteReader();

                userIdReader.Read();
                bookIdReader.Read();

                userID = (int)userIdReader["uID"]; // Variable set to userid
                bookID = (int)bookIdReader["bID"]; // Variable set to bookid

                // Command to add user score to database.
                OleDbCommand addRatingToDB = conn.CreateCommand();
                addRatingToDB.CommandText = "INSERT INTO Ratings (bookID, userID, rating) VALUES (@bookID, @userID, @rating)";
                addRatingToDB.Parameters.AddWithValue("@bookID", bookID);
                addRatingToDB.Parameters.AddWithValue("@userID", userID);
                addRatingToDB.Parameters.AddWithValue("@rating", rating);

                addRatingToDB.ExecuteNonQuery(); // User rating for specific book is saved to the database.
            }
            finally
            {
                Disconnect();
            }

        }

        // Returns the user rating. Also used to check if the user has rated the book. Returning 0 means user has not.
        [WebMethod]
        public int UserRated(string connectionString, string username, string bookname)
        {
            Connect(connectionString);

            // Command to get book id
            OleDbCommand commandBookName = conn.CreateCommand();
            commandBookName.CommandText = "SELECT * from Books WHERE bName = @name";
            commandBookName.Parameters.AddWithValue("@name", bookname);

            // Command to get user id
            OleDbCommand commandUserName = conn.CreateCommand();
            commandUserName.CommandText = "SELECT * from Users WHERE uUserName = @name";
            commandUserName.Parameters.AddWithValue("@name", username);


            // In order to retrieve the book rating, these two variables are required:
            int userID; // User id stored here. 
            int bookID; // Book id stored here
            try
            {
                OleDbDataReader bookIdReader = commandBookName.ExecuteReader();
                OleDbDataReader userIdReader = commandUserName.ExecuteReader();

                userIdReader.Read();
                bookIdReader.Read();

                userID = (int)userIdReader["uID"]; // Userid gets stored inside the variable.
                bookID = (int)bookIdReader["bID"]; // Bookid gets stored inside the variable.

                // Command to find user rating.
                OleDbCommand commandRating = conn.CreateCommand();
                commandRating.CommandText = "SELECT * FROM Ratings WHERE bookID = @bookID AND userID = @userID";
                commandRating.Parameters.AddWithValue("@bookID", bookID);
                commandRating.Parameters.AddWithValue("@userID", userID);

                OleDbDataReader ratingReader = commandRating.ExecuteReader();

                // Checks if a user rating exists. Meaning, if the user has rated the book. Returns the rating, or 0 if no rating was found.
                if (ratingReader.Read()) // User rating found.
                {
                    return (int)ratingReader["rating"]; // User rating returned.
                }
                else // User rating not found.
                {
                    return 0; // 0 returned because user has not rated the book.
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Returns the average rating of the book.
        [WebMethod]
        public double AverageRating(string connectionString, string bookname)
        {
            Connect(connectionString);

            // Command to get book id
            OleDbCommand commandBookName = conn.CreateCommand();
            commandBookName.CommandText = "SELECT * from Books WHERE bName = @name";
            commandBookName.Parameters.AddWithValue("@name", bookname);

            int bookID; // Stores the bookid for use later.

            double ratingSum = 0; // The sum of all ratings for a specific book.
            int count = 0; // The number of times a specific book has been rated.

            try
            {
                // Gets book id.
                OleDbDataReader bookIdReader = commandBookName.ExecuteReader();
                bookIdReader.Read();
                bookID = (int)bookIdReader["bID"]; // Variable set to book's id.

                // Command to select ratings of specified book.
                OleDbCommand commandRating = conn.CreateCommand();
                commandRating.CommandText = "SELECT * FROM Ratings WHERE bookID = @bookID";
                commandRating.Parameters.AddWithValue("@bookID", bookID);

                OleDbDataReader readerRating = commandRating.ExecuteReader();

                // Adds all ratings of the book to the RatingSum variable.
                // Will loop until all stored ratings for the book are read.
                while (readerRating.Read())
                {
                    ratingSum += double.Parse(readerRating["rating"].ToString()); // Rating added to the total.
                    count++; // Amount of times the book has been rated is increased by one.
                }
                
                // Returns the rating, or 0 if the book has not been rated.
                if (count != 0) // Book rated.
                {
                    return (ratingSum / count); // Returns the average rating of the book.
                }
                else // Book not rated.
                {
                    return 0;
                }
            }
            finally
            {
                Disconnect();
            }
        }

        // Returns user's name, surname and email as a list.
        [WebMethod]
        public List<string> UserDetails(string connectionString, string username)
        {
            List<string> details = new List<string>();

            Connect(connectionString);
            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM Users WHERE uUserName = @username";
            command.Parameters.AddWithValue("@username", username);

            try
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();

                // User details added to list. (In the order: name, surname, email)
                details.Add(reader["uName"].ToString());
                details.Add(reader["uSurname"].ToString());
                details.Add(reader["uEmail"].ToString());

                return details;
            }
            finally
            {
                Disconnect();
            }
        }

        // Updates user's personal information (password gets changed separately)
        [WebMethod]
        public void UpdateUserDetail(string connectionString, string username, string name, string surname, string email)
        {
            Connect(connectionString);

            OleDbCommand command = conn.CreateCommand();
            command.CommandText = "UPDATE Users SET uName = @name, uSurname = @surname, uEmail = @email WHERE uUserName = @username";
            command.Parameters.AddWithValue("@name", name);
            command.Parameters.AddWithValue("@surname", surname);
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@username", username);

            try
            {
                command.ExecuteNonQuery(); // User's personal details get updated.
            }
            finally
            {
                Disconnect();
            }
        }
        #endregion
    }
}