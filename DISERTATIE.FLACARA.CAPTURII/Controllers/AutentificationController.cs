using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Authentification;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
public class AutentificationController : ControllerBase
{
    private readonly IServiceAuthentification _authService;
    public AutentificationController(IServiceAuthentification authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDTO user)
    {
        try
        {
            return Ok(await _authService.RegisterAsync(user));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDTO user)
    {
        try
        {
            return Ok(await _authService.GenerateTokenAsync(user.UserName, user.Password));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
