using Microsoft.AspNetCore.Mvc;

namespace Fahrenheit451API.Controllers
{
    [ApiController]
    [Route("api/respond")]
    public class FahrenheitController : ControllerBase
    {
        private static int author = 0;
        private static bool limited = true;
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

        int step = 0;

        [HttpPost]
        public IActionResult Respond([FromBody] UserInput input)
        {
            string response = ProcessInput(input.Text);
            return Ok(new { response });
        }

        private string ProcessInput(string input)
        {
            if (!access) 
            {
                switch (step) 
                {
                    case 0:
                        if (input.ToLower() == "y") 
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
                            return "Yayy! Access granted.";
                        }
                        return "Incorrect title. Try again.";
                }
            }
            
            return "Access granted.";
        }

        private bool CheckForName(string input)
        {
            for (int i = 0; i < authors.Length; i++)
            {
                if (input == authors[i])
                {
                    author = i;
                    return true;
                }
            }
            return false;
        }

        private bool CheckForBook(string input)
        {
            if (input == books[author]) {
                return true;
            }
            else {
                return false;
            }
        }


        public class UserInput
        {
            public required string Text { get; set; }
        }
    }
}
