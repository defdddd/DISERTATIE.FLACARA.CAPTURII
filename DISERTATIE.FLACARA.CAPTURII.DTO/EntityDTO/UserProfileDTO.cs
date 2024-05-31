using DISERTATIE.FLACARA.CAPTURII.UTILS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

public record UserProfileDTO
{
    public int? Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? City { get; set; }
    public Gender Gender { get; set; }
    public Status Status { get; set; }
    public Role Role { get; set; }
    public string? Description { get; set; }

}
