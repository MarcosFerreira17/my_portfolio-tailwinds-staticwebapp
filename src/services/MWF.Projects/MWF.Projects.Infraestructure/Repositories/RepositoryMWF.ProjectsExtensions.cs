using System.Reflection;
using System.Text;
using MWF.Projects.Domain.Entities;

namespace MWF.Projects.Infraestructure.Repositories;

public static class RepositoryMWF.ProjectsExtensions
{
    public static IQueryable<MWF.ProjectsEntity> FilterMWF.Projects(this IQueryable<MWF.ProjectsEntity>
                                                            mwf.projects, uint maxId, uint minId) =>
                                                                mwf.projects.Where(e => e.Id >= maxId && e.Id <= minId);

    public static IQueryable<MWF.ProjectsEntity> Search(this IQueryable<MWF.ProjectsEntity> mwf.projects,
                                                    string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return mwf.projects;

        var lowerCaseTerm = searchTerm.Trim().ToLower();
        return mwf.projects.Where(e => e.ExampleString.ToLower().Contains(lowerCaseTerm));
    }

    // public static IQueryable<MWF.ProjectsEntity> Sort(this IQueryable<MWF.ProjectsEntity> mwf.projects,
    //                                                 string orderByQueryString)
    // {
    //     if (string.IsNullOrWhiteSpace(orderByQueryString))
    //         return mwf.projects.OrderBy(e => e.ExampleString);

    //     var orderParams = orderByQueryString.Trim().Split(',');
    //     var propertyInfos = typeof(MWF.ProjectsEntity)
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
    //         return mwf.projects.OrderBy(e => e.ExampleString);

    //     return mwf.projects.OrderBy(orderQuery);
    // }
}
