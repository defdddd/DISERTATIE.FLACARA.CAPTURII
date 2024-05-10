using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DISERTATIE.FLACARA.CAPTURII.Controllers;

[Route("FLACARA.CAPTURII/[controller]")]
[ApiController]
[Authorize]

public class PhotoController : ControllerBase
{
    private readonly IPhotoService photoService;
    private readonly IWebHostEnvironment? webHostEnvironment;
    private string path = string.Empty;
    public PhotoController(IPhotoService photoService, IWebHostEnvironment? webHostEnvironment)
    {
        this.photoService = photoService;
        this.webHostEnvironment = webHostEnvironment;
        this.path = webHostEnvironment.ContentRootPath + "/Images/{0}/{1}/";
    }

    #region Crud Operation
    [HttpPost("addPhoto")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateImages([FromForm] PhotoFormDTO model)
    {
        try
        {
            var photoDto = JsonConvert.DeserializeObject<PhotoDTO>(model.Photo);

            var file = model.File;

            var path = string.Format(this.path, photoDto.UserId, photoDto.Type);

            if (file.Length > 0)
            {
                await SavePicture(path, file);

                photoDto.URL = this.GetPicturesListAsync(photoDto.Type, photoDto.UserId).FirstOrDefault(x => x.Contains(file.FileName));

                photoDto.FileName = file.FileName;
            }

            return Ok(await this.photoService.InsertEntityAsync(photoDto));

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("getImages/{type}/{id}")]
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

    [HttpGet("getMyPhotos")]
    public async Task<IActionResult> GetMyPhotos()
    {
        try
        {
            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            var result = (await this.photoService.Entities())
                                                 .Where(x => x.UserId == userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("delete/{type}/{id}/{fileName}")]
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

    private async Task SavePicture(string path, IFormFile file)
    {
        Directory.CreateDirectory(path);

        using var fileStream = System.IO.File.Create(path + file.FileName);

        await file.CopyToAsync(fileStream);

        await fileStream.FlushAsync();
    }

    private List<string> GetPicturesListAsync(string type, int id)
    {
        var location = $"/Images/{id}/{type}/";

        var path = webHostEnvironment.ContentRootPath + location;

        var host = "https:" + Request.Host.Value + location;

        return Directory.GetFiles(path)
                        .Select(file => host + Path.GetFileName(file))
                        .ToList();

    }
    #endregion
}