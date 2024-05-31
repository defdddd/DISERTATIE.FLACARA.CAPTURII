using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS.ManualMappers;

internal static class UserUserDTO
{
    internal static UserProfileDTO MapToDTO(User user)
    {
        return new UserProfileDTO
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePicture = user.ProfilePicture,
            Description = user.Description,
            City = user.City,
            Gender = user.Gender,
            Status = user.Status,
            Role = user.Role
        };
    }
}
