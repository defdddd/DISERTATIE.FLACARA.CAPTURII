using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO;

public record LoginUserDTO
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}