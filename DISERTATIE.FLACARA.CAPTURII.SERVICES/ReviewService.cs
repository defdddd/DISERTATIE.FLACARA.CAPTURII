   using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class ReviewService : IReviewService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<ReviewDTO> _validator;
    private readonly IMapper _mapper;
    #endregion

    #region Constructor
    public ReviewService(IDataFactory repositories, IValidator<ReviewDTO> validator, IMapper mapper)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(ReviewDTO value)
    {
        var review = _mapper.Map<Review>(value);
        return await _repositories.ReviewRepository.DeleteAsync(review);
    }

    public async Task<List<ReviewDTO>> Entities()
    {
        var reviews = await _repositories.ReviewRepository.GetAllAsync();
        return _mapper.Map<List<ReviewDTO>>(reviews);
    }

    public async Task<ReviewDTO> InsertEntityAsync(ReviewDTO value)
    {
        var existingReview = await _repositories.ReviewRepository.FirstOrDefaultAsync(x => x.Text == value.Text && x.UserId == value.UserId);
        if (existingReview != null)
        {
            throw new ValidationException("Review already exists");
        }

        await Validate.FluentValidate(_validator, value);

        var review = _mapper.Map<Review>(value);

        var insertedReview = await _repositories.ReviewRepository.InsertAsync(review);
        return _mapper.Map<ReviewDTO>(insertedReview);
    }

    public async Task<ReviewDTO> SearchEntityByIdAsync(int id)
    {
        var reviewDto = await _repositories.ReviewRepository.SearchByIdAsync(id);
        return _mapper.Map<ReviewDTO>(reviewDto);
    }

    public async Task<ReviewDTO> UpdateEntityAsync(ReviewDTO value)
    {
        await Validate.FluentValidate(_validator, value);

        var review = await _repositories.ReviewRepository.UpdateAsync(_mapper.Map<Review>(value));
        return _mapper.Map<ReviewDTO>(review);
    }

    #endregion

    #region Additional Methods

    // No additional methods currently

    #endregion
}
