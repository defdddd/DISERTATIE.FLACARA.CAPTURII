using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO;

public record JwtDTO
{
    public required int Id { get; set; }
    public required string Token { get; set; }
}
