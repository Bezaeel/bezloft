using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography.X509Certificates;

namespace bezloft.application.Features.Events.Queries;

public class GetEventParticipantsQuery : IRequest<BaseResponse<List<GetEventParticipantsResponseDTO>>>
{
    public Guid Id { get; set; }
}

public class GetEventParticipantsDTO
{
    public Guid id { get; set; }
}

public class GetEventParticipantsResponseDTO
{
    public Guid id { get; set; }
}

public class GetEventParticipantsHandler : IRequestHandler<GetEventParticipantsQuery, BaseResponse<List<GetEventParticipantsResponseDTO>>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<GetEventParticipantsHandler> _logger;
    private readonly IMapper _mapper;

    public GetEventParticipantsHandler(IApplicationDbContext dbContext, ILogger<GetEventParticipantsHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse<List<GetEventParticipantsResponseDTO>>> Handle(GetEventParticipantsQuery request, CancellationToken cancellationToken)
    {
        var result = _dbContext.Events
            .Include(x => x.RSVPs)
            .Where(y => y.Id == request.Id)
            .Select(x => x.RSVPs)
            .ToList();

        var _ = result[0];

        var res = _mapper.Map<List<GetEventParticipantsResponseDTO>>(_);

        return new BaseResponse<List<GetEventParticipantsResponseDTO>>
        {
            Status = true,
            Message = "success",
            Data = res
        };
    }
}
