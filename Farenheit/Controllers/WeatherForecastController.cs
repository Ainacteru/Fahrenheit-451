using Microsoft.AspNetCore.Mvc;

namespace Fahrenheit451API.Controllers
{
    [ApiController]
    [Route("api/respond")]
    public class FahrenheitController : ControllerBase
    {
        private static int author = 0;
        private static bool limited = true;
        
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
            string response = ProcessInput(input.Text);
            return Ok(new { response });
        }

        private string ProcessInput(string input)
        {
            switch (input.ToLower())
            {
                case "y":
                    return "Please enter your name:";
                case "n":
                    return "Access Denied.";
                case "help":
                    return "Commands: help, get permission";
                case "get permission":
                    return "I rise from the ashes, reborn anew, in a world of fire, where books are few. Type 'Phoenix' to gain access.";
                case "phoenix":
                    limited = false;
                    return "Permission Granted.";
                default:
                    if (CheckForName(input))
                        return "Please name your assigned title:";
                    else if (limited)
                        return "Access limited. Type 'help' for commands.";
                    else
                        return "Access Denied.";
            }
        }

        private bool CheckForName(string input)
        {
            for (int i = 0; i < authors.Length; i++)
            {
                if (input.Equals(authors[i], StringComparison.OrdinalIgnoreCase))
                {
                    author = i;
                    return true;
                }
            }
            return false;
        }

        public class UserInput
        {
            public required string Text { get; set; }
        }
    }
}
