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

    public async Task<bool> DeleteEntityAsync(CommentDTO value)
    {
        var Comment = _mapper.Map<Comment>(value);
        return await _repositories.CommentRepository.DeleteAsync(Comment);
    }

    public async Task<List<CommentDTO>> Entities()
    {
        var Comments = await _repositories.CommentRepository.GetAllAsync();
        return _mapper.Map<List<CommentDTO>>(Comments);
    }

    public async Task<CommentDTO> InsertEntityAsync(CommentDTO value)
    {
        await Validate.FluentValidate(_validator, value);

        var Comment = _mapper.Map<Comment>(value);

        var insertedComment = await _repositories.CommentRepository.InsertAsync(Comment);
        return _mapper.Map<CommentDTO>(insertedComment);
    }

    public async Task<CommentDTO> SearchEntityByIdAsync(int id)
    {
        var CommentDto = await _repositories.CommentRepository.SearchByIdAsync(id);
        return _mapper.Map<CommentDTO>(CommentDto);
    }

    public async Task<CommentDTO> UpdateEntityAsync(CommentDTO value)
    {
        var CommentSearch = await _repositories.CommentRepository.SearchByIdAsync(value.Id);

        if (CommentSearch == null)
        {
            throw new ValidationException("Comment does not exist");
        }

        await Validate.FluentValidate(_validator, value);

        var Comment = await _repositories.CommentRepository.UpdateAsync(_mapper.Map<Comment>(value));
        return _mapper.Map<CommentDTO>(Comment);
    }

    #endregion

    #region Additional Methods

    // No additional methods currently

    #endregion
}
