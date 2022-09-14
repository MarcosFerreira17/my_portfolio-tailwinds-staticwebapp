using AutoMapper;
using MWF.Blog.Domain.Dtos;
using MWF.Blog.Domain.Entities;

namespace MWF.Blog.Infraestructure.Automapper;

public class MWF.BlogProfile : Profile
{
    public MWF.BlogProfile()
    {
        CreateMap<MWF.BlogDto, MWF.BlogEntity>().ReverseMap();
    }
}