using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS.DTO.Validation;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Authentification;

public class ServiceAuthentication : IServiceAuthentification
{
    private readonly IDataFactory _repositories;
    private readonly string _myKey;
    private readonly IMapper _mapper;

    public ServiceAuthentication(IDataFactory repositories, string myKey, IMapper mapper)
    {
        _myKey = myKey;
        _repositories = repositories;
        _mapper = mapper;

    }
    private async Task<bool> CheckEmailAsync(string email)
    {
        return await _repositories.UserRepository.FirstOrDefaultAsync(x => x.Email == email) != null;

    }

    private async Task<bool> CheckUserNameAsync(string userName)
    {
        return await _repositories.UserRepository.FirstOrDefaultAsync(x => x.UserName == userName) != null;
    }

    public async Task<dynamic> GenerateTokenAsync(string userName, string password)
    {
        var user = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.UserName == userName);

        if (user == null || user.Password != password)
        {
            return string.Empty;
        }

        var claims = new List<Claim>
        {
            new ("Identifier", user.Id.ToString()),
            new (ClaimTypes.Role, user.Role.ToString()),
            new (JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
            new (JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(7)).ToUnixTimeSeconds().ToString())
        };

        var token = new JwtSecurityToken(
            new JwtHeader(
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_myKey)),
                    SecurityAlgorithms.HmacSha256
                )
            ),
            new JwtPayload(claims)
        );

        var output = new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            UserId = user.Id
        };

        return output;
    }

    public async Task<UserDTO> RegisterAsync(UserDTO user)
    {
        if (await CheckEmailAsync(user.Email))
        {
            throw new ValidationException("Invalid email");
        }

        if (await CheckUserNameAsync(user.UserName))
        {
            throw new ValidationException("Invalid username");
        }

        user.Role = Role.User;
        user.Status = Status.NotConfirmed;

        var validator = new UserValidator();
        await Validate.FluentValidate(validator, user);

        var result = _mapper.Map<User>(user);

        return _mapper.Map<UserDTO>(await _repositories.UserRepository.InsertAsync(result));
    }
}
