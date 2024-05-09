using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
public class FolderController : ControllerBase
{
    private readonly IFolderService _folderService;
    private readonly IWebHostEnvironment? webHostEnvironment;
    private string path = string.Empty;


    public FolderController(IFolderService folderService, IWebHostEnvironment? webHostEnvironment)
    {
        _folderService = folderService;
        this.webHostEnvironment = webHostEnvironment;
        this.path = webHostEnvironment.ContentRootPath + "/Images/{0}/{1}/";

    }

    #region Crud Operation

    [HttpGet("all")]

    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(await _folderService.Entities());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("insert")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Insert([FromBody] FolderDTO folder)
    {
        try
        {
            var path = string.Format(this.path, folder.UserId, folder.Name);

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }         

            return Ok(await _folderService.InsertEntityAsync(folder));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("update")]
    [Authorize(Roles = "Admin,User")]

    public async Task<IActionResult> Update([FromBody] FolderDTO folder)
    {
        try
        {
            await CheckRole();

            return Ok(await _folderService.UpdateEntityAsync(folder));
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
            return Ok(await _folderService.DeleteEntityAsync(id));
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