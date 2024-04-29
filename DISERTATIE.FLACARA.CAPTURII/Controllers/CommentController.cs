using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    #region Crud Operation

    [HttpGet("all")]
   

    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _commentService.Entities());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Insert([FromBody] CommentDTO comment)
    {
        try
        {
            return Ok(await _commentService.InsertEntityAsync(comment));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] CommentDTO comment)
    {
        try
        {
            await CheckRole();

            return Ok(await _commentService.UpdateEntityAsync(comment));
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
            return Ok(await _commentService.DeleteEntityAsync(id));
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
