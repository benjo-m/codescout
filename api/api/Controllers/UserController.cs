using api.DTOs.User;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpGet("IsRecruiter")]
    public ActionResult<bool> IsRecruiter(int userId)
    {
        try
        {
            return Ok(_userService.IsRecruiter(userId));
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        List<UserDto> users = await _userService.GetAllUsers();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUserById(int id)
    {
        UserDto? user = await _userService.GetUserById(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpPost]
    public ActionResult<User> RegisterUser(UserCreateRequest userRequest)
    {
        User? newUser = _userService.RegisterUser(userRequest);

        if (newUser == null)
        {
            return BadRequest(newUser);
        }

        return Ok(newUser);
    }

    [HttpGet("stats")]
    public ActionResult<UserStats> GetUserStats([FromQuery] int userId)
    {
        return _userService.GetUserStats(userId);
    }

    [HttpPut("add-award")]
    public void AddAward(int userId, int awardId)
    {
        _userService.AddAward(userId, awardId);
    }

    [HttpPut("edit")]
    public ActionResult<User> EditUser(UserUpdateRequest request)
    {
        return _userService.EditProfile(request);
    }
}