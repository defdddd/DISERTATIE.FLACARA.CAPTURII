using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO;

public record PhotoDTO
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required string PhotoUrl { get; set; }
    public required string Description { get; set; }
}
