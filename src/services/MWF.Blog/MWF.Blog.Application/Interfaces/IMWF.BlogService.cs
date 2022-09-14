using MWF.Blog.Application.RequestFeatures;
using MWF.Blog.Domain.Common;
using MWF.Blog.Domain.Dtos;

namespace MWF.Blog.Domain.Interfaces;

public interface IMWF.BlogService : IBaseService<MWF.BlogDto, long>
{
    Task<PagedList<MWF.BlogDto>> PagedGetAll(MWF.BlogParameters MWF.BlogParameters);
}
