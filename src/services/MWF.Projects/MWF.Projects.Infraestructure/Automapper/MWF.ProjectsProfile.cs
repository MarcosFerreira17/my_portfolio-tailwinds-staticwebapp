using AutoMapper;
using MWF.Projects.Domain.Dtos;
using MWF.Projects.Domain.Entities;

namespace MWF.Projects.Infraestructure.Automapper;

public class MWF.ProjectsProfile : Profile
{
    public MWF.ProjectsProfile()
    {
        CreateMap<MWF.ProjectsDto, MWF.ProjectsEntity>().ReverseMap();
    }
}