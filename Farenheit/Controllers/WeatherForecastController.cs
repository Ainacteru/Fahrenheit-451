using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Fahrenheit451API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        static int author = 0;
        static string[] books = {
            "Gulliver's Travels", "On the Origin of Species, The World as Will and Representation",
            "Relativity: The Special and the General Theory", "The Philosophy of Civilization", 
            "The Clouds", "The Story of My Experiments with Truth", "Dhammapada", "Analects",
            "Nightmare Abbey", "The Declaration of Independence", "The Gettysburg Address", 
            "Common Sense", "The Prince", "The Bible"
        };

        static string[] authors = {
            "Jonathan Swift", "Charles Darwin", "Schopenhauer", "Einstein", "Albert Schweitzer",
            "Aristophanes", "Mahatma Gandhi", "Gautama Buddha", "Confucius", "Thomas Love Peacock",
            "Thomas Jefferson", "Lincoln", "Tom Paine", "Machiavelli", "Christ"
        };

        static bool limited = true;

        [HttpPost("respond")]
        public ActionResult Respond([FromBody] UserInput userInput)
        {
            string response = "";

            if (userInput.Text.ToLower() == "y")
            {
                response = "Please enter your name.";
            }
            else if (CheckForName(userInput.Text))
            {
                response = "Please name your assigned title.";
            }
            else
            {
                response = "Access Denied.";
            }

            return Ok(new { response });
        }

        private bool CheckForName(string answer)
        {
            for (int i = 0; i < authors.Length; i++)
            {
                if (answer == authors[i])
                {
                    author = i;
                    return true;
                }
            }
            return false;
        }
    }

    public class UserInput
    {
        public string Text { get; set; }
    }
}
