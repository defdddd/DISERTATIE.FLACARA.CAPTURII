using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO;

public record PhotoDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string URL { get; set; }
    public string Type { get; set; }
    public string FileName { get; set; }
    public IFormFile? File { get; set; }

}
