using bezloft.application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace bezloft.application.Common.Utils;

public static class Paginate
{
    public static async Task<PagedModel<TModel>> PaginateAsync<TModel>(
        this IQueryable<TModel> query,
        int page,
        int limit,
        CancellationToken cancellationToken)
        where TModel : class
    {
        var paged = new PagedModel<TModel>();

        page = (page < 1) ? 1 : page;
        limit = (limit < 1) ? 10 : limit;

        paged.meta.CurrentPage = page;
        paged.meta.PageSize = limit;

        var startRow = (page - 1) * limit;
        paged.Items = await query
            .Skip(startRow)
            .Take(limit)
            .ToListAsync(cancellationToken);

        paged.meta.TotalRecords =  query.Count();
        paged.meta.TotalPages = (int)Math.Ceiling(paged.meta.TotalRecords / (double)limit);

        return paged;
    }
    
    public static PagedModel<TModel> PaginateList<TModel>(
        this IList<TModel> query,
        int page,
        int limit)
        where TModel : class
    {
        var paged = new PagedModel<TModel>();

        page = (page < 1) ? 1 : page;
        limit = (limit < 1) ? 10 : limit;

        paged.meta.CurrentPage = page;
        paged.meta.PageSize = limit;

        var startRow = (page - 1) * limit;
        paged.Items = query
            .Skip(startRow)
            .Take(limit).ToList();

        paged.meta.TotalRecords =  query.Count();
        paged.meta.TotalPages = (int)Math.Ceiling(paged.meta.TotalRecords / (double)limit);

        return paged;
    }
}