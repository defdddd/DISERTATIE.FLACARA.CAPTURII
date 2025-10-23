using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
///
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


    [HttpGet("getPosts/{page}/{pageSize}")]
    public async Task<IActionResult> GetPosts(int page, int pageSize)
    {
        try
        {
            var result = await this.photoService.GetPosts(page, pageSize);
                                                 
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("getUsersPosts/{page}/{pageSize}/{lastName}")]
    public async Task<IActionResult> GetUsersPosts(int page, int pageSize, string lastName)
    {
        try
        {
            var result = await this.photoService.GetUsersPosts(page, pageSize, lastName);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            return Ok(await this.photoService.SearchEntityByIdAsync(id));   
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    [HttpPut("update")]

    public async Task<IActionResult> Update([FromBody] PhotoDTO photo)
    {
        try
        {
            await CheckRole();

            return Ok(await this.photoService.UpdateEntityAsync(photo));
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("top10Posts")]
    public async Task<IActionResult> GetTop10Posts()
    {
        try
        {
            var result = await this.photoService.GetTop10Posts();

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }



    #region Crud Operation
    [HttpPost("addPhoto")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> UpdateImages([FromForm] PhotoFormDTO model)
    {
        try
        {
            var photoDto = JsonConvert.DeserializeObject<PhotoDTO>(model.Photo);

            var userId = int.Parse(User.FindFirst("Identifier")?.Value);

            photoDto.UserId = userId;

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


    [HttpGet("top10UsersPosts/{userId}")]
    public async Task<IActionResult> GetTop10UsersPosts(int userId)
    {
        try
        {
            var result = await this.photoService.GetTop10UsersPosts(userId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,User,Photographer")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        try
        {
            var photo = await this.photoService.SearchEntityByIdAsync(id);

            var path = webHostEnvironment.ContentRootPath + $"/Images/{photo.UserId}/{photo.Type}/{photo.FileName}";

            System.IO.File.Delete(path);

            return Ok(await this.photoService.DeleteEntityAsync(photo.Id.GetValueOrDefault()));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private List<string> GetPicturesListAsync(string type, int id)
    {
        var location = $"/Images/{id}/{type}/";

        var path = webHostEnvironment.ContentRootPath + location;

        var host = Request.Scheme + ":" + Request.Host.Value + location;

        return Directory.GetFiles(path)
                        .Select(file => host + Path.GetFileName(file))
                        .ToList();

    }

    private async Task CheckRole()
    {

    }


    private async Task SavePicture(string path, IFormFile file, int width = 1920, int height = 1080, int quality = 75)
    {
        // Creează directorul dacă nu există
        Directory.CreateDirectory(path);

        // Crează calea completă pentru fișier
        var filePath = Path.Combine(path, file.FileName);

        using (var stream = new MemoryStream())
        {
            // Copiază conținutul fișierului în MemoryStream
            await file.CopyToAsync(stream);

            // Crează imaginea din stream
            var srcImage = Image.FromStream(stream);

            // Redimensionează imaginea
            var resizedImage = ResizeImage(srcImage, width, height);

            // Salvează imaginea redimensionată cu calitatea specificată
            SaveJpeg(filePath, resizedImage, quality);
        }
    }

    private static Image ResizeImage(Image srcImage, int width, int height)
    {
        var destRect = new Rectangle(0, 0, width, height);
        var destImage = new Bitmap(width, height);

        destImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using (var wrapMode = new ImageAttributes())
            {
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(srcImage, destRect, 0, 0, srcImage.Width, srcImage.Height, GraphicsUnit.Pixel, wrapMode);
            }
        }

        return destImage;
    }

    private static void SaveJpeg(string path, Image image, int quality)
    {
        var encoder = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
        var encoderParameters = new EncoderParameters(1);
        encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);
        image.Save(path, encoder, encoderParameters);
    }


    #endregion
}