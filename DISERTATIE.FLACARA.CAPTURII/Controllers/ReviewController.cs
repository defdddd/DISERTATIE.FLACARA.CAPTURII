using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;
    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    #region Crud Operation

    [HttpGet("all")]

    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _reviewService.Entities());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Insert([FromBody] ReviewDTO review)
    {
        try
        {
            return Ok(await _reviewService.InsertEntityAsync(review));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] ReviewDTO review)
    {
        try
        {
            await CheckRole();

            return Ok(await _reviewService.UpdateEntityAsync(review));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            return Ok(await _reviewService.DeleteEntityAsync(id));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    private async Task CheckRole()
    {

    }
    #endregion
}
