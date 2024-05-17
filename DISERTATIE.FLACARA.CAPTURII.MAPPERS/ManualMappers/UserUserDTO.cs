using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS.ManualMappers;

internal static class UserUserDTO
{
    internal static UserDTO MapToDTO(User user)
    {
        return new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePicture = user.ProfilePicture,
            City = user.City,
            Gender = user.Gender,
            Status = user.Status,
            Role = user.Role
        };
    }
}
