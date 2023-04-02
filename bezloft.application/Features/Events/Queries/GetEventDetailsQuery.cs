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
    public string name { get; set; }
}

public class GetEventDetailsQuery : IRequest<BaseResponse<GetEventDetailsResponseDTO>>
{
    public Guid Id { get; set; }
}

public class GetEventDetailsHandler : IRequestHandler<GetEventDetailsQuery, BaseResponse<GetEventDetailsResponseDTO>>
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

    public async Task<BaseResponse<GetEventDetailsResponseDTO>> Handle(GetEventDetailsQuery request, CancellationToken cancellationToken)
    {
        var entity = _dbContext.Events
            .FirstOrDefault(x => x.Id == request.Id);

        var result = _mapper.Map<GetEventDetailsResponseDTO>(entity);

        return new BaseResponse<GetEventDetailsResponseDTO>
        {
            Status = true,
            Message = "success",
            Data = result
        };
    }
}
