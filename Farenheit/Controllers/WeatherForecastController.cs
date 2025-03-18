using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

namespace Fahrenheit451API.Controllers
{
    [ApiController]
    [Route("api/respond")]
    public class FahrenheitController : ControllerBase
    {
        // Path to the folder containing the text files
        private readonly string _textFileDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/TextFiles");
        
        private static bool limited = true;
        private static bool access = false;

        private static string status = "";
        private static int author = 0;

        private static int step = 0;

        private static readonly string[] books = {
            "Gulliver's Travels",
            "On the Origin of Species", 
            "The World as Will and Representation",
            "Relativity: The Special and the General Theory",
            "The Philosophy of Civilization",
            "The Clouds",
            "The Story of My Experiments with Truth",
            "Dhammapada",
            "Analects",
            "Nightmare Abbey",
            "The Declaration of Independence",
            "The Gettysburg Address",
            "Common Sense",
            "The Prince",
            "The Bible"
        };

        private static readonly string[] authors = {
            "Jonathan Swift", "Charles Darwin", "Schopenhauer", "Einstein", "Albert Schweitzer",
            "Aristophanes", "Mahatma Gandhi", "Gautama Buddha", "Confucius", "Thomas Love Peacock",
            "Thomas Jefferson", "Lincoln", "Tom Paine", "Machiavelli", "Christ"
        };

        [HttpPost]
        public IActionResult Respond([FromBody] UserInput input)
        {
            //int step = HttpContext.Session.GetInt32("step") ?? 0;

            string response = ProcessInput(input.Text);

            // HttpContext.Session.SetInt32("step", step);  // Store updated step

            return Ok(new { response });
        }

        private string ProcessInput(string answer)
        {
            try {
                string input = answer.ToLower();
                if (!access) 
                {
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
                default:
                    if (isFullPermissionGranted(input)) {
                        return "Full Access Granted";
                    } else if (input.StartsWith("open ")) {
                        // Remove the "open " prefix and match the remaining part with book titles
                        string bookTitle = input.Substring(5);  // Removes the "open " part
                        return OpenBook(bookTitle);
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
                    default:
                        if (input.StartsWith("open ") )
                        {
                            // Remove the "open " prefix and match the remaining part with book titles
                            string bookTitle = input.Substring(5);  // Removes the "open " part
                            return OpenBook(bookTitle);
                        }
                        return "Not a valid command. Type a 'help' for a list of commands"; 
                }
        }

        private string OrginizationMembers() {

            if(!limited) {
                return "";
            }
            return "No access to this information";
        }

        private string OpenBook(string input) {

            try {
                // Get all .txt files in the directory
                string[] files = Directory.GetFiles(_textFileDirectory, "*.txt");

                // Find a case-insensitive match
                string? matchingFile = files
                    .FirstOrDefault(f => Path.GetFileNameWithoutExtension(f).Equals(input, StringComparison.OrdinalIgnoreCase));

                if (matchingFile != null)
                {
                    if (limited && (matchingFile == books[author])) 
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

        private string AvailableBooks(){
            if (limited) {
                return "You have access to 1 book(s):\n" + books[author];
            }
            return "you have access to 14 book(s):\n" + string.Join("\n", books);
        }

        private string PermissionRiddle()
        {
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

        private string GetHelp()
        {
            string additionalStuff;
            if (limited) {
                additionalStuff = "get permission - Request additional access\n" + 
                                  "mission - Shows what we are trying to do";
            } else {
                additionalStuff = "";
            }

            return "Available commands:\n" + 
                        "help - Opens this menu\n" +
                        "books - Lists all books available to you\n" +
                        additionalStuff;
        }

        private bool CheckForName(string input)
        {
            for (int i = 0; i < authors.Length; i++)
            {
                if (input == authors[i].ToLower())
                {
                    author = i;
                    return true;
                }
            }
            return false;
        }

        private bool CheckForBook(string input)
        {
            if (input == books[author].ToLower()) {
                return true;
            }
            else {
                return false;
            }
        }

        private string SignIn(string input) {
            
            switch (step) {
                case 0:
                    if(input == "y") {step = 3; limited = false; access = true; return "COOLEST PERSON IN THE WORLD!!";}
                    if (input == "continue") 
                    {
                        step++;
                        return "Please enter your name:";
                    }
                    return "Access Denied.";

                case 1:
                    if (CheckForName(input)) 
                    {
                        step++;
                        return "Please enter your assigned title:";
                    }
                    else if (input == "ari") {return "ew";}
                    
                    return "Invalid name. Try again.";

                case 2:
                    if (CheckForBook(input)) 
                    {
                        step++;
                        access = true;
                        return "Access granted. You can access 1 book(s).   Type 'help' for a list of commands";
                    }
                    return "Incorrect title. Try again.";
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
