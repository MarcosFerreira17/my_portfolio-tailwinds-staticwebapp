using MWF.Projects.Application.RequestFeatures;
using MWF.Projects.Domain.Common;
using MWF.Projects.Domain.Dtos;

namespace MWF.Projects.Domain.Interfaces;

public interface IMWF.ProjectsService : IBaseService<MWF.ProjectsDto, long>
{
    Task<PagedList<MWF.ProjectsDto>> PagedGetAll(MWF.ProjectsParameters MWF.ProjectsParameters);
}
