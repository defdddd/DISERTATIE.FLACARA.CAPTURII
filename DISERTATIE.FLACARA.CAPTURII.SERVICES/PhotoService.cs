using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.HUBS;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using DISERTATIE.FLACARA.CAPTURII.UTILS;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS;
using FluentValidation;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES;

public class PhotoService : IPhotoService
{
    #region Fields
    private readonly IDataFactory _repositories;
    private readonly IValidator<PhotoDTO> _validator;
    private readonly IMapper _mapper;
    private readonly IHubContext<PostHub> _hubContext;

    #endregion

    #region Constructor
    public PhotoService(IDataFactory repositories, IValidator<PhotoDTO> validator, IMapper mapper, IHubContext<PostHub> hubContext)
    {
        _repositories = repositories;
        _validator = validator;
        _mapper = mapper;
        _hubContext = hubContext;
    }
    #endregion

    #region Crud Methods

    public async Task<bool> DeleteEntityAsync(int id)
    {
        var photo = await _repositories.PhotoRepository.FirstOrDefaultAsync(x => x.Id == id);
        return await _repositories.PhotoRepository.DeleteAsync(photo);
    }

    public async Task<List<PhotoDTO>> Entities()
    {
        var photos = await _repositories.PhotoRepository.GetAllAsync();
        return _mapper.Map<List<PhotoDTO>>(photos);
    }
    public async Task<List<PhotoDTO>> GetUsersPosts(int page, int pageSize, string lastName)
    {
        var result = await this.EntitiesWithPagination(page, pageSize, lastName);

        foreach (var photo in result)
        {
            var reviews = await this._repositories.ReviewRepository.GetByPhotoId(photo.Id.GetValueOrDefault());
            var comments = await this._repositories.CommentRepository.GetByPhotoId(photo.Id.GetValueOrDefault());

            photo.Comments = _mapper.Map<List<CommentDTO>>(comments);
            photo.Reviews = _mapper.Map<List<ReviewDTO>>(reviews);
        }

        return result;
    }

    public async Task<List<PhotoDTO>> EntitiesWithPagination(int page, int pageSize)
    {
        var photos = await _repositories.PhotoRepository.GetPagedData(page, pageSize);

        return _mapper.Map<List<PhotoDTO>>(photos);
    }

    public async Task<List<PhotoDTO>> EntitiesWithPagination(int page, int pageSize, string lastName)
    {
        var photos = await _repositories.PhotoRepository.GetPagedData(page, pageSize, lastName);

        return _mapper.Map<List<PhotoDTO>>(photos);
    }

    public async Task<List<PhotoDTO>> GetPosts(int page, int pageSize)
    {
        var result = await this.EntitiesWithPagination(page, pageSize);

        foreach(var photo in result)
        {
            var reviews = await this._repositories.ReviewRepository.GetByPhotoId(photo.Id.GetValueOrDefault());
            var comments = await this._repositories.CommentRepository.GetByPhotoId(photo.Id.GetValueOrDefault());

            photo.Comments = _mapper.Map<List<CommentDTO>>(comments);
            photo.Reviews = _mapper.Map<List<ReviewDTO>>(reviews);
        }

        return result;
    }

    public async Task<dynamic> GetTop10Posts()
    {
        var result = (await _repositories.PhotoRepository.GetTop10Posts())
            .Select(x =>
            new
            {
                Id = x.PhotoId,
                URL = x.PhotoURL,
                AverageGrade = x.AverageGrade,
            });


        return result.ToList();
    }


    public async Task<PhotoDTO> InsertEntityAsync(PhotoDTO value)
    {

        if (string.IsNullOrEmpty(value.URL))
        {
            throw new ValidationException("Invalid Photo");
        }

        var existentFile = await _repositories.PhotoRepository.FirstOrDefaultAsync(x => x.UserId == value.UserId && 
                                                                                        x.URL == value.URL && 
                                                                                        x.FileName == value.FileName);

        if (existentFile != null)
        {
            var existentFileResult = _mapper.Map<PhotoDTO>(existentFile);
            var id = existentFile.Id;

            var comments = _mapper.Map<List<CommentDTO>>(await this._repositories.CommentRepository.GetByPhotoId(id));
            var reviews = _mapper.Map<List<ReviewDTO>>(await this._repositories.ReviewRepository.GetByPhotoId(id));

            existentFileResult.Reviews = reviews;
            existentFileResult.Comments = comments;

            await _hubContext.Clients.All.SendAsync("NewPostAdded", existentFileResult);

            return existentFileResult;
        }


        var folder = await _repositories.FolderRepository.FirstOrDefaultAsync(x => x.Name == value.Type) ??
                     await _repositories.FolderRepository.InsertAsync(new Folder() { Name = value.Type, UserId = value.UserId});

        value.FolderId = folder.Id;

        await Validate.FluentValidate(_validator, value);

        var photo = _mapper.Map<Photo>(value);

        var insertedPhoto = await _repositories.PhotoRepository.InsertAsync(photo);
        var result = _mapper.Map<PhotoDTO>(insertedPhoto);

        await _hubContext.Clients.All.SendAsync("NewPostAdded", result);

        return result;
    }

    public async Task<PhotoDTO> SearchEntityByIdAsync(int id)
    {
        var photoDto = await _repositories.PhotoRepository.SearchByIdAsync(id);
        return _mapper.Map<PhotoDTO>(photoDto);
    }

    public async Task<PhotoDTO> UpdateEntityAsync(PhotoDTO value)
    {
        var photoSearch = await _repositories.PhotoRepository.SearchByIdAsync(value.Id.GetValueOrDefault());

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
