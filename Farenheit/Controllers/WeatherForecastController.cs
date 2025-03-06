using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Farenheit.Controllers;

[ApiController]
[Route("api/respond")]
public class InteractionController : ControllerBase
{
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
    public required string Text { get; set; }
}
