using Microsoft.AspNetCore.Mvc;

namespace Farenheit.Controllers;

[ApiController]
[Route("api/respond")]
public class InteractionController : ControllerBase
{
    public static int author = 0;
    static string[] books = {
            "Gulliver's Travels", // Jonathan Swift
            "On the Origin of Species, The World as Will and Representation", // Charles Darwin, //  Schopenhauer
            "On the Origin of Species, The World as Will and Representation", // Charles Darwin, Schopenhauer
            "Relativity: The Special and the General Theory", // Einstein
            "The Philosophy of Civilization", // Albert Schweitzer
            "The Clouds", // Aristophanes
            "The Story of My Experiments with Truth", // Mahatma Gandhi
            "Dhammapada", // Gautama Buddha
            "Analects", // Confucius
            "Nightmare Abbey", // Thomas Love Peacock
            "The Declaration of Independence", // Thomas Jefferson
            "The Gettysburg Address", // Lincoln
            "Common Sense", // Tom Paine
            "The Prince", // Machiavelli
            "The Bible" // Christ
        };
    static string[] authors = {
        "Jonathan Swift", 
        "Charles Darwin", 
        "Schopenhauer", 
        "Einstein", 
        "Albert Schweitzer", 
        "Aristophanes", 
        "Mahatma Ghandi", 
        "Gautama Buddha", 
        "Confucius", 
        "Thomas Love Peacock", 
        "Thomas Jefferson", 
        "Lincoln", 
        "Tom Paine", 
        "Machiavelli", 
        "Christ"
    };
    static bool limited = true;
        

    // [HttpPost]
    // public IActionResult Respond([FromBody] UserInput input)
    // {
    //     string response = ProcessInput(input.Text);
    //     return Ok(new { response });
    // }
    // private string ProcessInput(string text)
    // {
    //     // Replace this with your existing logic
    //     return "You said: " + text;
    // }

    [HttpPost]
        public ActionResult Respond([FromBody] UserInput userInput)
        {
            string response = "Welcome to my Farhenheit 451 thingy";
            int progression = 0;

            if ((userInput.Text == "continue") && (progression == 0))
            {
                response = "Please enter your name.";
                progression++;
            }
            else if((progression == 1) && CheckForName(userInput.Text)) 
            {
                response = "Please name your assigned title.";
                progression++;
            }
            else if((progression == 2) && CheckForBook(userInput.Text))
            {
                response = "next part frfr";
                progression++;
            }
            else
            {
                response = "not a valid command";
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

        private bool CheckForBook(string answer)
        {

            if (answer == books[author])
            {
                return true;
            }
            else 
            {
                return false;
            }
                
        }

}

public class UserInput
{
    public required string Text { get; set; }
}
