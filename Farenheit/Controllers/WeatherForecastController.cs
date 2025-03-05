using Microsoft.AspNetCore.Mvc;

namespace Farenheit.Controllers;

[ApiController]
[Route("api/respond")]
public class InteractionController : ControllerBase
{
    [HttpPost]
    public IActionResult Respond([FromBody] UserInput input)
    {
        string response = ProcessInput(input.Text);
        return Ok(new { response });
    }

    private string ProcessInput(string text)
    {
        // Replace this with your existing logic
        return "You said: " + text;
    }
}

public class UserInput
{
    public required string Text { get; set; }
}