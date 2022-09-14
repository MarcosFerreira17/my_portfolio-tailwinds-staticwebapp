using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MWF.Projects.Application.Exceptions;
using MWF.Projects.Application.RequestFeatures;
using MWF.Projects.Domain.Dtos;
using MWF.Projects.Domain.Entities;
using MWF.Projects.Domain.Interfaces;
using MWF.Projects.Infraestructure.Repositories;

namespace MWF.Projects.Domain.Services;

public class MWF.ProjectsService : IMWF.ProjectsService
{
    protected readonly IMWF.ProjectsRepository _repo;
    protected readonly IMapper _mapper;
    protected readonly ILogger<MWF.ProjectsService> _logger;
    // protected readonly IValidator<MWF.ProjectsEntity> _validator;
    public MWF.ProjectsService(IMWF.ProjectsRepository repo, IMapper mapper, ILogger<MWF.ProjectsService> logger)
    {
        _repo = repo;
        // _validator = validator;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task Create(MWF.ProjectsDto mwf.projectsDto)
    {
        var entityfromdb = _mapper.Map<MWF.ProjectsEntity>(mwf.projectsDto);
        // var validation = await _validator.ValidateAsync(entityfromdb);

        // if (!validation.IsValid)
        // {
        //     _logger.LogError("Incorret format validation.");
        //     throw new GenericException();
        // }
        await _repo.Create(entityfromdb);
    }

    public async Task Update(long id, MWF.ProjectsDto mwf.projectsDto)
    {
        await SearchForExistingId(id);
        if (id != mwf.projectsDto.Id)
        {
            _logger.LogError("Parameter and request id must match");
            throw new NotFoundException("Parameter and request id must match.");
        }
        var updateEntity = _mapper.Map<MWF.ProjectsEntity>(mwf.projectsDto);
        await _repo.Update(updateEntity);
    }

    public async Task Delete(long id)
    {
        var entity = await SearchForExistingId(id);
        await _repo.Delete(entity);
    }

    public async Task<PagedList<MWF.ProjectsDto>> PagedGetAll(MWF.ProjectsParameters mwf.projectsParameters)
    {
        if (!mwf.projectsParameters.ValidIdRange) throw new BadRequestException("MinId cannot be greater than MaxId");

        var entities = await _repo.FindByCondition(x => x.Id >= 0)
                            .FilterMWF.Projects(mwf.projectsParameters.MinId, mwf.projectsParameters.MaxId)
                            .Search(mwf.projectsParameters.SearchTerm)
                            .OrderBy(t => t.Id)
                            // .Sort(mwf.projectsParameters.OrderBy)
                            .ToListAsync();

        var pagedEntities = _mapper.Map<List<MWF.ProjectsDto>>(entities);
        return PagedList<MWF.ProjectsDto>.ToPagedList(pagedEntities, mwf.projectsParameters.PageNumber, mwf.projectsParameters.PageSize);
    }

    public async Task<IEnumerable<MWF.ProjectsDto>> GetAll()
    {
        var entities = await _repo.FindAll();
        var mappedEntity = _mapper.Map<IEnumerable<MWF.ProjectsDto>>(entities);
        if (entities == null)
        {
            _logger.LogInformation("No data found.");
            throw new NotFoundException("No data found.");
        }
        return mappedEntity;
    }

    public async Task<MWF.ProjectsDto> GetById(long id)
    {
        var entityFromDb = await SearchForExistingId(id);

        return _mapper.Map<MWF.ProjectsDto>(entityFromDb);
    }

    protected async Task<MWF.ProjectsEntity> SearchForExistingId(long id)
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

        return _mapper.Map<MWF.ProjectsEntity>(entityFromDb);
    }
}