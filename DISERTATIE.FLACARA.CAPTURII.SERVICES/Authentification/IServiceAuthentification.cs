using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Authentification;

public interface IServiceAuthentification
{
    Task<dynamic> GenerateTokenAsync(string userName, string password);
    Task<UserDTO> RegisterAsync(UserDTO user);
    Task SendEmail(int id);
    Task<bool> CheckKey(string key, int userId);
}
