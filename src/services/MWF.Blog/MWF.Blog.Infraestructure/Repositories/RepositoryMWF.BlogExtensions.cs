using System.Reflection;
using System.Text;
using MWF.Blog.Domain.Entities;

namespace MWF.Blog.Infraestructure.Repositories;

public static class RepositoryMWF.BlogExtensions
{
    public static IQueryable<MWF.BlogEntity> FilterMWF.Blog(this IQueryable<MWF.BlogEntity>
                                                            mwf.blog, uint maxId, uint minId) =>
                                                                mwf.blog.Where(e => e.Id >= maxId && e.Id <= minId);

    public static IQueryable<MWF.BlogEntity> Search(this IQueryable<MWF.BlogEntity> mwf.blog,
                                                    string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return mwf.blog;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return mwf.blog.Where(e => e.ExampleString.ToLower().Contains(lowerCaseTerm));
    }

    // public static IQueryable<MWF.BlogEntity> Sort(this IQueryable<MWF.BlogEntity> mwf.blog,
    //                                                 string orderByQueryString)
    // {
    //     if (string.IsNullOrWhiteSpace(orderByQueryString))
    //         return mwf.blog.OrderBy(e => e.ExampleString);

    //     var orderParams = orderByQueryString.Trim().Split(',');
    //     var propertyInfos = typeof(MWF.BlogEntity)
    //         .GetProperties(BindingFlags.Public | BindingFlags.Instance);
    //     var orderQueryBuilder = new StringBuilder();

    //     foreach (var param in orderParams)
    //     {
    //         if (string.IsNullOrWhiteSpace(param)) continue;

    //         var propertyFromQueryName = param.Split(" ")[0];

    //         var objectProperty = Array.Find(propertyInfos, pi =>
    //             pi.Name.Equals(
    //                 propertyFromQueryName,
    //                 StringComparison.InvariantCultureIgnoreCase));

    //         if (objectProperty == null) continue;

    //         var direction = param.EndsWith(" desc") ? "descending" : "ascending";
    //         orderQueryBuilder.Append(objectProperty.Name).Append(' ').Append(direction).Append(", ");
    //     }

    //     var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

    //     if (string.IsNullOrWhiteSpace(orderQuery))
    //         return mwf.blog.OrderBy(e => e.ExampleString);

    //     return mwf.blog.OrderBy(orderQuery);
    // }
}
