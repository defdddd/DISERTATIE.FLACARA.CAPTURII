using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class PhotoService : IPhotoService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<PhotoDTO> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public PhotoService(IDataFactory repositories, IValidator<PhotoDTO> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(PhotoDTO value)
    {
        var photo = _mapper.Map<Photo>(value);
        return await _repositories.PhotoRepository.DeleteAsync(photo);
    }

    public async Task<List<PhotoDTO>> Entities()
    {
        var photos = await _repositories.PhotoRepository.GetAllAsync();
        return _mapper.Map<List<PhotoDTO>>(photos);
    }

    public async Task<PhotoDTO> FirstOrDefaultAsync(Func<PhotoDTO, bool> expression)
    {
        var expressionMapped = _mapper.Map<Func<Photo, bool>>(expression);

        var resultMapped = await _repositories.PhotoRepository.FirstOrDefaultAsync(expressionMapped);

        return _mapper.Map<PhotoDTO>(resultMapped);
    }

    public async Task<PhotoDTO> InsertEntityAsync(PhotoDTO value)
    {
        var existingPhoto = await _repositories.PhotoRepository.FirstOrDefaultAsync(x => x.PhotoUrl == value.PhotoUrl && x.UserId == value.UserId);
        if (existingPhoto != null)
        {
            throw new ValidationException("Photo already exists");
        }

        await Validate.FluentValidate(_validator, value);

        var photo = _mapper.Map<Photo>(value);

        var insertedPhoto = await _repositories.PhotoRepository.InsertAsync(photo);
        return _mapper.Map<PhotoDTO>(insertedPhoto);
    }

    public async Task<PhotoDTO> SearchEntityByIdAsync(int id)
    {
        var photoDto = await _repositories.PhotoRepository.SearchByIdAsync(id);
        return _mapper.Map<PhotoDTO>(photoDto);
    }

    public async Task<PhotoDTO> UpdateEntityAsync(PhotoDTO value)
    {
        var photoSearch = await _repositories.PhotoRepository.SearchByIdAsync(value.Id);

        if (photoSearch == null)
        {
            throw new ValidationException("Photo does not exist");
        }

        await Validate.FluentValidate(_validator, value);

        var photo = await _repositories.PhotoRepository.UpdateAsync(_mapper.Map<Photo>(value));
        return _mapper.Map<PhotoDTO>(photo);
    }

    #endregion

    #region Additional Methods

    // No additional methods currently

    #endregion
}
