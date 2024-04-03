using DISERTATIE.FLACARA.CAPTURII.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;

public interface IUserService : IBaseService<UserDTO>
{
    Task<UserDTO> SearchByEmailAsync(string email);
    Task<UserDTO> SearchByUserNameAsync(string userName);
}
