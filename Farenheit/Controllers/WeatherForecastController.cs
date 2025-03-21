using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using Fahrenheit451API.SessionExtensions;

namespace Fahrenheit451API.Controllers
{
    [ApiController]
    [Route("api/respond")]
    public class FahrenheitController : ControllerBase
    {

        
         // Get session values or set defaults
        private bool limited {
            get {
                var value = HttpContext.Session.GetBoolean("limited");

                if (value == null)
                {
                    Console.WriteLine("Session 'limited' value not found, defaulting to false.");
                    return true; // Default to false if not set
                }

                return value.Value;
            }
            set {
                HttpContext.Session.SetBoolean("limited", value);
            }
        }

        private bool access {
            get {
                var value = HttpContext.Session.GetBoolean("access");

                if (value == null)
                {
                    Console.WriteLine("Session 'access' value not found, defaulting to false.");
                    return false; // Default to false if not set
                }

                return value.Value;
            }
            set {
                HttpContext.Session.SetBoolean("access", value);
            }
        }

        private string status {
            get => HttpContext.Session.GetString("status") ?? "";
            set => HttpContext.Session.SetString("status", value);
        }

        private int author {
            get => HttpContext.Session.GetInt32("author") ?? 0;
            set => HttpContext.Session.SetInt32("author", value);
        }

        private int step {
            get => HttpContext.Session.GetInt32("step") ?? 0;
            set => HttpContext.Session.SetInt32("step", value);
        }

       Random rnd = new Random();

        [HttpPost]
        public IActionResult Respond([FromBody] UserInput input) {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("status"))) {
                status = "welcome";  // Ensure it's set for new sessions
            }

            if ((step == 0) && (status == "welcome")) {
                status = "enter";
                return Ok(new { message = "Welcome! Type 'continue' to continue." });
            }

            string response = ProcessInput(input.Text);
            return Ok(new { message = response }); // Always return "message"
        }

        private string ProcessInput(string answer) {
            try {
                string input = answer.ToLower();
                if (!access) {
                    return SignIn(input);
                }
                else if (limited) {
                    return LessAccess(input);
                }
                else {
                    return FullAccess(input);
                }
            }
            catch (Exception e) {
                return "Internal Error: " + e.Message;
            }
        }

        private string LessAccess(string input) {
            switch (input) {
                case "help":
                    return GetHelp();
                case "get permission":
                    return PermissionRiddle();
                case "books":
                    return AvailableBooks();
                case "mission":
                    return "Our mission is to rebuild the country so that it is once again has strength through knowledge";
                case "members":
                    return OrginizationMembers();
                case "books burnt":
                    return booksBurned();
                case "hound status":
                    return houndStatus();
                default:
                    if (isFullPermissionGranted(input)) {
                        return "Full Access Granted";
                    } else if (input.StartsWith("open ")) {
                        // Remove the "open " prefix and match the remaining part with book titles
                        string bookTitle = input.Substring(5);  // Removes the "open " part
                        return OpenBook(bookTitle);
                    } else if (input.StartsWith("pass down ")) {
                        string bookTitle = input.Substring(10);
                        return passDown(bookTitle);
                    }
                    
                    return "Not a valid command. Type a 'help' for a list of commands";
            }
        }

        private string FullAccess(string input) {
                switch (input) {
                    case "help":
                        return GetHelp();
                    case "books":
                        return AvailableBooks();
                    case "mission":
                        return "Our mission is to rebuild the country so that it is once again has strength through knowledge";
                    case "members":
                        return OrginizationMembers();
                    case "books burnt":
                        return booksBurned();
                    case "hound status":
                        return houndStatus();
                    case "search firemen":
                        return searchFiremen();         
                    default:
                        if (input.StartsWith("open ") )
                        {
                            // Remove the "open " prefix and match the remaining part with book titles
                            string bookTitle = input.Substring(5);  // Removes the "open " part
                            return OpenBook(bookTitle);

                        } else if (input.StartsWith("pass down ")) {

                            string bookTitle = input.Substring(10);
                            return passDown(bookTitle);

                        } else if (input.StartsWith("broadcast ")) {

                            string message = input.Substring(10);
                            return broadcast(message);

                        } else if (input.StartsWith("burn ")) {

                            string message = input.Substring(5);
                            return burnBook(message);

                        }
                        return "Not a valid command. Type a 'help' for a list of commands"; 
                }
        }

      

        private string GetHelp() {
            string additionalStuff;
            if (limited) {
                additionalStuff = "get permission - Request additional access\n";
            } else {
                additionalStuff = "burn {book} - Removes a book from the database\n" +
                                  "members - Returns information of people working to achieve the mission goal\n" +
                                  "search firemen - Returns if known firemen are a threat to the organization\n" +
                                  "broadcast {message} - Sends a message to other rebellion organizations\n";
            }
            //secret command: Clarisse - Returns a philosophical question or observation in the style of clarisse
            return "Available commands:\n" + 
                        "help - Opens this menu\n" +
                        "mission - Shows what we are trying to do\n" +
                        "books - Lists all books available to you\n" +
                        "open {book} - Shows you the contents of the book that was requested\n" +
                        "books burnt - Shows you how many books have been burned\n" +
                        "pass down {book} - Passes down the book to the next generation\n" +
                        "hound status - Returns what the closest mechanical hound is currently doing\n" +
                        //"\n" +
                        additionalStuff;
        }

        public string burnBook(string input) {

            for (int i = 0; i < Database.books.Length; i++) {

                if (input == Database.books[i].ToLower()) {

                    Database.books[i] = ""; 
                    Console.WriteLine(Database.authors[author] + " has burnt the book: " + input);
                    return "The book '" + input + "' has been burnt and is no longer accessible. \n" +
                           "You can no longer read the book or log in";
                }

            }
            
            return "The book you wanted to burn was not found in the database. Please use the 'books' command and choose a valid book";

        }

        private string searchFiremen() {
            return "Beatty - Firemen Captain\n" +
                   "       - Status: Dead\n\n" +
                   "Black  - Fireman\n" +
                   "       - Status: Arrested for carrying books\n\n" +
                   "Stoneman - Fireman\n" +
                   "         - Status: Probably a bad driver\n\n" +
                   "Montag - Fireman\n" +
                   "         - Status: Not a fireman anymore\n";
            
        }

        private string broadcast(string input) {
            Console.WriteLine(Database.authors[author] + " has sent the message: " + input);
            return "'" + input + "' was succesfully sent";
        }

        private string houndStatus() {
            int num = rnd.Next(1, 150);
            return "Hound " + ((num % 2) + 8) + " is " + num + " km away";
        }

        private string passDown(string book) {
            foreach (var books in Database.books) {
                if (book == books.ToLower()) {
                    return book + " has been passed down to your children";
                }
            }
            return "Book was not found in the database, please use the 'books' command and choose a valid book";
        }

        private string booksBurned() {
            return "We have no idea how many books have actually been burnt, we just know that there are too many books burnt that we can't count it";
        }
        private string OrginizationMembers() {

            if(!limited) {
                return "Granger - Writer of the book 'The Fingers in the Glove; the Proper Relationship between the Individual and Society'\n" +
                        "Fred Clement -  former occupant of the Thomas Hardy chair at Cambridge\n" +
                        "Dr. Simmons - Specialist in Ortega y Gasset\n" +
                        "Professor West - Taught ethics at Columbia University\n" +
                        "Guy Montag - Former fireman\n"
                ;
            }
            return "No access to this information";
        }

        private string OpenBook(string input) {

            try {
                // Get all .txt files in the directory
                string[] files = Directory.GetFiles(Database._textFileDirectory, "*.txt");

                // Find a case-insensitive match
                string? matchingFile = files
                    .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f).Equals(input, StringComparison.OrdinalIgnoreCase));

                if (matchingFile != null)
                {
                    if (limited && (matchingFile == Database.books[author])) 
                    {
                        return System.IO.File.ReadAllText(matchingFile);
                    }
                    return System.IO.File.ReadAllText(matchingFile);
                }

                return "Text not found in Database";
                // string[] files = Directory.GetFiles(_textFileDirectory, "*.txt");
                // return "Files found:\n" + string.Join("\n", files);
            }
            catch (Exception ex)
            {
                // Log or return a more specific error message
                return $"Error occurred: {ex.Message}";
            }
        }

        private string AvailableBooks() {
            if (limited) {
                return "You have access to 1 book(s):\n" + Database.books[author];
            }

            int count = 0;

            foreach (string book in Database.books)
            {
                if (!string.IsNullOrEmpty(book))
                {
                    count++;
                }
            }
            return "you have access to " + count + " book(s):\n" + string.Join("\n", Database.books);
        }

        private string PermissionRiddle() {
            status = "getting permission";
            return "I rise from the ashes, reborn again, where books are a symbol of hope, when all seems lost, I'm reborn, no matter what";
        }

        private bool isFullPermissionGranted(string input) {
            if (input == "phoenix" && status == "getting permission") {
                limited = false;
                status = "";
                return true;
            }
            status = "";
            return false;
        }

        private bool CheckForName(string input) {
            for (int i = 0; i < Database.authors.Length; i++)
            {
                if (input == Database.authors[i].ToLower())
                {
                    author = i;
                    return true;
                }
            }
            return false;
        }

        private bool CheckForBook(string input) {
            if (!string.IsNullOrEmpty(input) && input == Database.books[author].ToLower()) {
                return true;
            }
            return false;
        }

        private string SignIn(string input) {
            
            switch (step) {
                case 0:
                    //if(input == "y") {step = 3; limited = false; access = true; return "COOLEST PERSON IN THE WORLD!!";}
                    if (input == "continue") 
                    {
                        step++;
                        return "Please enter your name:";
                    }
                    return "Please type 'continue' to proceed";

                case 1:
                    try { 
                        if (CheckForName(input)) 
                        {
                            step++;
                            return "Please enter your assigned title:";
                        }
                        else if (input == "ari") {
                            throw new StackOverflowException("OH EW!");
                        }
                        
                        return "Invalid name. Try again.";
                    }
                    catch (Exception e) {
                        return e.Message;
                    }

                case 2:
                    if (CheckForBook(input)) 
                    {
                        step++;
                        access = true;
                        return "Access granted. You can access 1 book(s).   Type 'help' for a list of commands";
                    }
                    step--;
                    return "Incorrect title. Please type in your name";
                default:
                    return "";

            }
        }
    }
    
    public class UserInput
    {
        public required string Text { get; set; }
    }
}
