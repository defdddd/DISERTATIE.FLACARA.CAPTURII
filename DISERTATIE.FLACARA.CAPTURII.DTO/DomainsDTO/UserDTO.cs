using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;

public record UserDTO : RegisterUserDTO
{
    public string? ProfilePicture { get; set; }
    public string? City { get; set; }
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Role Role { get; set; }
}
