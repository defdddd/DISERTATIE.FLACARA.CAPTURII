using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

public record PhotoFormDTO
{
    public string Photo { get; set; }
    public IFormFile File { get; set; }
}
