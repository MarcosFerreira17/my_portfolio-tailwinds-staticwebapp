using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MWF.Blog.Application.Exceptions;
using MWF.Blog.Application.RequestFeatures;
using MWF.Blog.Domain.Dtos;
using MWF.Blog.Domain.Entities;
using MWF.Blog.Domain.Interfaces;
using MWF.Blog.Infraestructure.Repositories;

namespace MWF.Blog.Domain.Services;

public class MWF.BlogService : IMWF.BlogService
{
    protected readonly IMWF.BlogRepository _repo;
    protected readonly IMapper _mapper;
    protected readonly ILogger<MWF.BlogService> _logger;
    // protected readonly IValidator<MWF.BlogEntity> _validator;
    public MWF.BlogService(IMWF.BlogRepository repo, IMapper mapper, ILogger<MWF.BlogService> logger)
    {
        _repo = repo;
        // _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Create(MWF.BlogDto mwf.blogDto)
    {
        var entityfromdb = _mapper.Map<MWF.BlogEntity>(mwf.blogDto);
        // var validation = await _validator.ValidateAsync(entityfromdb);

        // if (!validation.IsValid)
        // {
        //     _logger.LogError("Incorret format validation.");
        //     throw new GenericException();
        // }
        await _repo.Create(entityfromdb);
    }

    public async Task Update(long id, MWF.BlogDto mwf.blogDto)
    {
        await SearchForExistingId(id);
        if (id != mwf.blogDto.Id)
        {
            _logger.LogError("Parameter and request id must match");
            throw new NotFoundException("Parameter and request id must match.");
        }
        var updateEntity = _mapper.Map<MWF.BlogEntity>(mwf.blogDto);
        await _repo.Update(updateEntity);
    }

    public async Task Delete(long id)
    {
        var entity = await SearchForExistingId(id);
        await _repo.Delete(entity);
    }

    public async Task<PagedList<MWF.BlogDto>> PagedGetAll(MWF.BlogParameters mwf.blogParameters)
    {
        if (!mwf.blogParameters.ValidIdRange) throw new BadRequestException("MinId cannot be greater than MaxId");

        var entities = await _repo.FindByCondition(x => x.Id >= 0)
                            .FilterMWF.Blog(mwf.blogParameters.MinId, mwf.blogParameters.MaxId)
                            .Search(mwf.blogParameters.SearchTerm)
                            .OrderBy(t => t.Id)
                            // .Sort(mwf.blogParameters.OrderBy)
                            .ToListAsync();

        var pagedEntities = _mapper.Map<List<MWF.BlogDto>>(entities);
        return PagedList<MWF.BlogDto>.ToPagedList(pagedEntities, mwf.blogParameters.PageNumber, mwf.blogParameters.PageSize);
    }

    public async Task<IEnumerable<MWF.BlogDto>> GetAll()
    {
        var entities = await _repo.FindAll();
        var mappedEntity = _mapper.Map<IEnumerable<MWF.BlogDto>>(entities);
        if (entities == null)
        {
            _logger.LogInformation("No data found.");
            throw new NotFoundException("No data found.");
        }
        return mappedEntity;
    }

    public async Task<MWF.BlogDto> GetById(long id)
    {
        var entityFromDb = await SearchForExistingId(id);

        return _mapper.Map<MWF.BlogDto>(entityFromDb);
    }

    protected async Task<MWF.BlogEntity> SearchForExistingId(long id)
    {
        if (id < 1)
        {
            _logger.LogError("Field id must be filled.");
            throw new NotFoundException("Field id must be filled and has to be greater than 0.");
        }

        var entityFromDb = await _repo.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();
        if (entityFromDb is null)
        {
            _logger.LogInformation("Id not found");
            throw new NotFoundException("This id does not exist in our database, please check and try again.");
        }

        return _mapper.Map<MWF.BlogEntity>(entityFromDb);
    }
}