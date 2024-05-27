using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class CommentService : ICommentService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<CommentDTO> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public CommentService(IDataFactory repositories, IValidator<CommentDTO> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(int id)
    {
        var comment = await _repositories.CommentRepository.FirstOrDefaultAsync(x => x.Id == id);
        return await _repositories.CommentRepository.DeleteAsync(comment);
    }

    public async Task<List<CommentDTO>> Entities()
    {
        var comments = await _repositories.CommentRepository.GetAllAsync();
        return _mapper.Map<List<CommentDTO>>(comments);
    }

    public Task<List<CommentDTO>> EntitiesWithPagination(int page, int pageSize)
    {
        throw new NotImplementedException();
    }

    public async Task<CommentDTO> InsertEntityAsync(CommentDTO value)
    {
        await Validate.FluentValidate(_validator, value);

        var comment = _mapper.Map<Comment>(value);

        var insertedComment = await _repositories.CommentRepository.InsertAsync(comment);
        return _mapper.Map<CommentDTO>(insertedComment);
    }

    public async Task<CommentDTO> SearchEntityByIdAsync(int id)
    {
        var commentDto = await _repositories.CommentRepository.SearchByIdAsync(id);
        return _mapper.Map<CommentDTO>(commentDto);
    }

    public async Task<CommentDTO> UpdateEntityAsync(CommentDTO value)
    {
        var commentSearch = await _repositories.CommentRepository.SearchByIdAsync(value.Id.GetValueOrDefault());

        if (commentSearch == null)
        {
            throw new ValidationException("Comment does not exist");
        }

        await Validate.FluentValidate(_validator, value);

        var comment = await _repositories.CommentRepository.UpdateAsync(_mapper.Map<Comment>(value));
        return _mapper.Map<CommentDTO>(comment);
    }

    #endregion

    #region Additional Methods

    // No additional methods currently

    #endregion
}
