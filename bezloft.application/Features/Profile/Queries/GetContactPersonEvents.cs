using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.application.Common.Utils;
using bezloft.core.Entities;
using bezloft.core.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Profile.Queries;

public class GetContactPersonEventsResponseDTO
{
    public Guid id { get; set; }
}
public class GetContactPersonEventsQuery : IRequest<BaseResponse<PagedModel<GetContactPersonEventsResponseDTO>>>
{
    public int page { get; set; }
    public int limit { get; set; }
    public Guid id { get; set; }
    public GetContactPersonEventsQueryFilter? filter { get; set; }
}

public class GetContactPersonEventsQueryFilter
{
    public EventVisibility visibility { get; set; }
}

public class GetContactPersonEventsHandler : IRequestHandler<GetContactPersonEventsQuery, BaseResponse<PagedModel<GetContactPersonEventsResponseDTO>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetContactPersonEventsHandler> _logger;
    private readonly IMapper _mapper;

    public GetContactPersonEventsHandler(IApplicationDbContext dbContext, ILogger<GetContactPersonEventsHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PagedModel<GetContactPersonEventsResponseDTO>>> Handle(GetContactPersonEventsQuery request, CancellationToken cancellationToken)
    {
        var query = filter(request);

        var _ = query.Select(x => new GetContactPersonEventsResponseDTO
            {
                id = x.Id
            });

        var result = await _.PaginateAsync(request.page, request.limit, cancellationToken);

        return new BaseResponse<PagedModel<GetContactPersonEventsResponseDTO>>
        {
            Status = true,
            Message = "success",
            Data = result
        };
    }

    public IQueryable<Event> filter(GetContactPersonEventsQuery request)
    {
        IQueryable<Event> query = _dbContext.Events
            .Where(x => x.ContactPersonId == request.id);

        if (request.filter?.visibility != null)
        {
            query = query.Where(x => x.Visibility == request.filter.visibility);
        }

        return query;
    }
}