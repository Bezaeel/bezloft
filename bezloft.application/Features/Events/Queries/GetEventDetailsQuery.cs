using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Events.Queries;

public class GetEventDetailsDTO
{
    public Guid Id { get; set; }
}

public class GetEventDetailsResponseDTO
{
    public Guid id { get; set; }
}

public class GetEventDetailsQuery : IRequest<BaseResponse<List<GetEventDetailsResponseDTO>>>
{
    public Guid Id { get; set; }
}

public class GetEventDetailsHandler : IRequestHandler<GetEventDetailsQuery, BaseResponse<List<GetEventDetailsResponseDTO>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetEventDetailsHandler> _logger;
    private readonly IMapper _mapper;

    public GetEventDetailsHandler(IApplicationDbContext dbContext, ILogger<GetEventDetailsHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<GetEventDetailsResponseDTO>>> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = _dbContext.Events
            .Where(x => x.Id == request.Id)
            .ToList();

        var result = _mapper.Map<List<GetEventDetailsResponseDTO>>(entity);

        return new BaseResponse<List<GetEventDetailsResponseDTO>>
        {
            Status = true,
            Message = "success",
            Data = result
        };
    }
}
