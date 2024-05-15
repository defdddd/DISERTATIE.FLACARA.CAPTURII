using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;

public record ReviewDTO
{
    public required int Id { get; set; }
    public required int PhotoId { get; set; }
    public required int UserId { get; set; }
    public required int Grade { get; set; }
    public required string Text { get; set; }
    public UserDTO? User { get; set; }

}
