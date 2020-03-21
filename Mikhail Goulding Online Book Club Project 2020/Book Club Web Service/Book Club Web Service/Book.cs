/*
 * Filename: Books.cs
 * Author: Mikhail Goulding
 * Date Created: 17 March 2020
 * Operating System: Windows 10
 * Version: 1.0
 * Description: This class stores the details of a book, in order to use it as a return object.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Club_Web_Service
{
    // Class storing the details of a book, in order to use it as a return object.
    public class Book
    {
        public string name = ""; // Book name
        public string author = ""; // Book author
        public string ISBN = ""; // Book isbn
        public int pages = 1; // Number of pages the book has
    }
}