using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    #region Crud Operation

    [HttpGet("all")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _userService.Entities());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Insert([FromBody] UserDTO user)
    {
        try
        {
            return Ok(await _userService.InsertEntityAsync(user));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] UserDTO user)
    {
        try
        {
            await CheckRole(user);

            return Ok(await _userService.UpdateEntityAsync(user));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            //var user = new UserDTO();

            //return Ok(await _userService.DeleteEntityAsync(user));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        return BadRequest();
    }

    #endregion

    #region Other Operation
    [HttpGet("username/{userName}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> SearchByUserName(string userName)
    {
        try
        {
            var person = await _userService.SearchByUserNameAsync(userName);
            return Ok(person);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("myProfile")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> GetMyData()
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            var person = await _userService.FirstOrDefaultAsync(x => x.Id == userId);

            return Ok(person);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpGet("email/{email}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> SearchByEmail(string email)
    {
        try
        {
            var person = await _userService.SearchByEmailAsync(email);
            return Ok(person);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    #endregion

    #region Private methods
    private async Task CheckRole(UserDTO user)
    {
        var userId = int.Parse(User.FindFirst("Identifier")?.Value);
        var userData = await _userService.SearchEntityByIdAsync(user.Id);
        var role = User.FindFirst(ClaimTypes.Role)?.Value;

        if (userId.Equals(user.Id) && userData.Role != user.Role)
            throw new ValidationException("You can't edit your role, contact the owner for this task");

        if (user.Id != userId && role != Role.Admin.ToString())
            throw new ValidationException("You don't have access to modify, view or insert this value");

    }
    #endregion
}