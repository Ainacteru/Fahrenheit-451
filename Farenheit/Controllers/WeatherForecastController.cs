using Microsoft.AspNetCore.Mvc;

namespace Fahrenheit451API.Controllers
{
    [ApiController]
    [Route("api/respond")]
    public class FahrenheitController : ControllerBase
    {
        private static int author = 0;
        private static bool limited = true;
        private static string status = "";
        private static bool access = false;
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
            int step = HttpContext.Session.GetInt32("step") ?? 0;

            string response = ProcessInput(input.Text, ref step);

            HttpContext.Session.SetInt32("step", step);  // Store updated step

            return Ok(new { response });
        }

        private string ProcessInput(string input, ref int step)
        {
            if (!access) 
            {
                switch (step) 
                {
                    case 0:
                        if (input.ToLower() == "continue") 
                        {
                            step++;
                            return "Please enter your name:";
                        }
                        return "Access Denied.";

                    case 1:
                        if (CheckForName(input.ToLower())) 
                        {
                            step++;
                            return "Please enter your assigned title:";
                        }
                        return "Invalid name. Try again.";

                    case 2:
                        if (CheckForBook(input.ToLower())) 
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
            else if (limited) {
                
                switch (input.ToLower())
                {
                    case "help":
                        return GetHelp();
                    case "get permission":
                        return PermissionRiddle();
                    case "books":
                        return AvailableBooks();
                    default:
                        if (isFullPermissionGranted(input.ToLower())) {
                            return "Full Access Granted";
                        }
                        return "Not a valid command. Type a 'help' for a list of commands";
                }
            }
            else {
                switch (input) {
                    case "help":
                        return GetHelp();
                    default:
                        return "Not a valid command. Type a 'help' for a list of commands"; 
                }
            }
        }

        private string AvailableBooks(){
            if (limited) {
                return "You have access to 1 book(s):/n" + books[author];
            }
            return "you have access to 14 book(s):/n" + string.Join("/n", books);

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
            if (limited)
            {
                return "Available commands:\n" + 
                        "help - Opens this menu\n" +
                        "get permission - Request additional access\n";
            }

            return "Available commands:\n" + 
                    "help - Opens this menu\n";
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


        
    }
    public class UserInput
    {
        public required string Text { get; set; }
    }
}
