using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService photoService;
    private readonly IWebHostEnvironment? webHostEnvironment;
    private string path = "{0}/Images/{1}/{2}/";
    public PhotoController(IPhotoService photoService, IWebHostEnvironment? webHostEnvironment)
    {
        this.photoService = photoService;
        this.webHostEnvironment = webHostEnvironment;
        this.path = string.Format(this.path, webHostEnvironment.ContentRootPath);
    }

    #region Crud Operation
    [HttpPost("UpdateImage")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateImages([FromForm] PhotoDTO file)
    {
        try
        {
            var path = string.Format(this.path, file.Type, file.Id);

            if (file.File.Length > 0)
            {
                await SavePicture(path, file);

                file.URL = this.GetPicturesListAsync(file.Type, file.Id).FirstOrDefault();

                file.FileName = file.File.FileName;
            }

            return Ok(await this.photoService.InsertEntityAsync(file));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetImages/{type}/{id}")]
    public async Task<IActionResult> GetFilesLocations(string type, int userId)
    {
        try
        {
            var result = (await this.photoService.Entities())
                                                 .Where(x => x.Type == type && x.UserId == userId);
                                                
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("DeleteImage/{type}/{id}/{fileName}")]
    [Authorize(Roles = "Admin,User")]
    public IActionResult DeleteFile(string type, int id, string fileName)
    {
        try
        {
            var path = webHostEnvironment.ContentRootPath + $"/Images/{type}/{id}/{fileName}";

            System.IO.File.Delete(path);

            return Ok(true);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private async Task SavePicture(string path, PhotoDTO photoDTO)
    {
        Directory.CreateDirectory(path);

        using var fileStream = System.IO.File.Create(path + photoDTO.File.FileName);

        await photoDTO.File.CopyToAsync(fileStream);

        await fileStream.FlushAsync();
    }

    private List<string> GetPicturesListAsync(string type, int id)
    {
        var location = $"/Images/{type}/{id}/";

        var path = webHostEnvironment.ContentRootPath + location;

        var host = "https:" + Request.Host.Value + location;

        return Directory.GetFiles(path)
                        .Select(file => host + Path.GetFileName(file))
                        .ToList();

    }
    #endregion
}