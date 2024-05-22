using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.HUBS;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class FolderService : IFolderService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<FolderDTO> _validator;
    private readonly IMapper _mapper;

    #endregion

    #region Constructor
    public FolderService(IDataFactory repositories, IValidator<FolderDTO> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(int id)
    {
        var comment = await _repositories.FolderRepository.FirstOrDefaultAsync(x => x.Id == id);
        return await _repositories.FolderRepository.DeleteAsync(comment);
    }

    public async Task<List<FolderDTO>> Entities()
    {
        var folders = await _repositories.FolderRepository.GetAllAsync();
        return _mapper.Map<List<FolderDTO>>(folders);
    }

    public Task<List<FolderDTO>> EntitiesWithPagination(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<List<FolderWithPhotosDTO>> GetMyFoldersWithPhotos(int userId)
    {
        var folders = await _repositories.FolderRepository.GetEntitiesWhereAsync(x => x.UserId == userId);

        var pictures = await _repositories.PhotoRepository.GetEntitiesWhereAsync(x => x.UserId == userId);

        var results = folders.Select(folder => new FolderWithPhotosDTO
        {
            Id = folder.Id,
            Name = folder.Name,
            Photos = _mapper.Map<List<PhotoDTO>>(pictures.Where(x => x.Type == folder.Name))
        }).ToList();

        return results;
    }

    public async Task<FolderDTO> InsertEntityAsync(FolderDTO value)
    {
        await Validate.FluentValidate(_validator, value);

        var comment = _mapper.Map<Folder>(value);

        var insertedFolder = await _repositories.FolderRepository.InsertAsync(comment);
        return _mapper.Map<FolderDTO>(insertedFolder);
    }

    public async Task<FolderDTO> SearchEntityByIdAsync(int id)
    {
        var commentDto = await _repositories.FolderRepository.SearchByIdAsync(id);
        return _mapper.Map<FolderDTO>(commentDto);
    }

    public async Task<FolderDTO> UpdateEntityAsync(FolderDTO value)
    {
        var commentSearch = await _repositories.FolderRepository.SearchByIdAsync(value.Id);

        if (commentSearch == null)
        {
            throw new ValidationException("Folder does not exist");
        }

        await Validate.FluentValidate(_validator, value);

        var comment = await _repositories.FolderRepository.UpdateAsync(_mapper.Map<Folder>(value));
        return _mapper.Map<FolderDTO>(comment);
    }

    #endregion

    #region Additional Methods

    // No additional methods currently

    #endregion
}
