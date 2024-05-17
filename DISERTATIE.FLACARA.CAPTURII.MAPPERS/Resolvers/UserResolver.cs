using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.MAPPERS.ManualMappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS.Resolvers;

public static class UserResolver
{
    public static UserDTO Resolve(IDataFactory dataFactory, int userId)
    {
        var usertaks = dataFactory.UserRepository.SearchByIdAsync(userId);
        usertaks.Wait();

        return UserUserDTO.MapToDTO(usertaks.Result);
    }
}
