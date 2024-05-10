using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class UserService : IUserService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<UserDTO> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public UserService(IDataFactory repositories, IValidator<UserDTO> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(int id)
    {
        var user = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.Id == id);
        return await _repositories.UserRepository.DeleteAsync(user);
    }

    public async Task<List<UserDTO>> Entities()
    {
        var users = await _repositories.UserRepository.GetAllAsync();
        return _mapper.Map<List<UserDTO>>(users);
    }

    public async Task<UserDTO> InsertEntityAsync(UserDTO value)
    {
        var existingUser = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.UserName.Equals(value.UserName));
        if (existingUser != null)
        {
            throw new ValidationException("User already exists");
        }

        await Validate.FluentValidate(_validator, value);

        var user = _mapper.Map<User>(value);
        user.Role = Role.User;
        user.Status = Status.NotConfirmed;

        var insertedUser = await _repositories.UserRepository.InsertAsync(user);
        return _mapper.Map<UserDTO>(insertedUser);
    }

    public async Task<UserDTO> SearchEntityByIdAsync(int id)
    {
        var userDto = await _repositories.UserRepository.SearchByIdAsync(id);
        return _mapper.Map<UserDTO>(userDto);
    }

    public async Task<UserDTO> UpdateEntityAsync(UserDTO value)
    {
        var existingUser = await _repositories.UserRepository.SearchByIdAsync(value.Id) ?? throw new ValidationException("User does not exist");

        var userWithSameUserName = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.UserName == value.UserName && x.Id != value.Id);
        if (userWithSameUserName != null)
        {
            throw new ValidationException("This username is already taken");
        }

        await Validate.FluentValidate(_validator, value);

        var updatedUser = await _repositories.UserRepository.UpdateAsync(_mapper.Map<User>(value));
        return _mapper.Map<UserDTO>(updatedUser);
    }

    #endregion

    #region Additional Methods

    public async Task<UserDTO> SearchByEmailAsync(string email)
    {
        var userDto = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.Email == email);
        return _mapper.Map<UserDTO>(userDto);
    }

    public async Task<UserDTO> SearchByUserNameAsync(string userName)
    {
        var userDto = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.UserName == userName);
        return _mapper.Map<UserDTO>(userDto);
    }

    public async Task<UserDTO> FirstOrDefaultAsync(Func<UserDTO, bool> expression)
    {
        var expressionMapped = _mapper.Map<Func<User, bool>>(expression);

        var resultMapped = await _repositories.UserRepository.FirstOrDefaultAsync(expressionMapped);

        return _mapper.Map<UserDTO>(resultMapped);
    }

    public async Task<UserProfileDTO> FirstOrDefaultAsync(int userId)
    {
        var result = await _repositories.UserRepository.FirstOrDefaultAsync(x => x.Id == userId);

        return _mapper.Map<UserProfileDTO>(result);
    }

    #endregion

}
