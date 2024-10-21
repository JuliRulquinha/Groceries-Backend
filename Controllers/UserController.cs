using Groceries.DTO;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers;
using BCrypt.Net;

[Route("[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository repository;
    public UserController(IUserRepository _repository)
    {
        repository = _repository;
    }

    [HttpPost("Register")]

    public IActionResult Register([FromBody] UserCreationDto dto)
    {
        var hashedPassword = BCrypt.HashPassword(dto.Password);
        dto.Password = hashedPassword;
        var newUser = dto.Adapt<User>();          
        var user = repository.Register(newUser);

        return Ok("User registered successfully.");
    }

    [HttpGet("Authenticate")]

    public IActionResult Authenticate(UserCreationDto dto)
    {
        var user = dto.Adapt<User>();
        var existingUser = repository.Authenticate(user);

        if (existingUser is null)
        {
            return NotFound("User not found");
        }
        else if (BCrypt.Verify(dto.Password, existingUser.Password))
        {
            return Ok("User found");
        }
        else
        {
            return BadRequest("User or password not found or incorrect.");
        }

    }
}

