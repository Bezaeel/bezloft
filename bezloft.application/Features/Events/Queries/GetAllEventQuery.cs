using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.application.Common.Utils;
using bezloft.core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Events.Queries;


public class GetAllEventQueryQuery : IRequest<BaseResponse<PagedModel<Event>>>
{
    public int page { get; set; }
    public int limit { get; set; }
}

public class GetAllEventQueryHandler : IRequestHandler<GetAllEventQueryQuery, BaseResponse<PagedModel<Event>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetAllEventQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetAllEventQueryHandler(IApplicationDbContext dbContext, ILogger<GetAllEventQueryHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse<PagedModel<Event>>> Handle(GetAllEventQueryQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Events;

        var result = await query.PaginateAsync(request.page, request.limit, cancellationToken);

        return new BaseResponse<PagedModel<Event>>
        {
            Status = true,
            Message = "success",
            Data = result
        };
    }
}
