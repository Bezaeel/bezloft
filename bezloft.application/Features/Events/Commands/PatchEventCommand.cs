using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.core.Entities;
using bezloft.core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Events.Commands;

public class PatchEventCommandDTO
{
    public string? description { get; set; }
    public EventVisibility? visibility { get; set; }
    public int? limit { get; set; }
    public DateTime? start { get; set; }
    public DateTime? end { get; set; }
}

public class PatchEventCommandCommand : IRequest<BaseResponse>
{
    public Guid Id { get; set; }
    public int? Limit { get; set; }
    public string? Description { get; set; }
    public EventVisibility? Visibility { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}

public class PatchEventCommandCommandValidator : AbstractValidator<PatchEventCommandCommand>
{
    public PatchEventCommandCommandValidator()
    {
    }
}

public class PatchEventCommandCommandHandler : IRequestHandler<PatchEventCommandCommand, BaseResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<PatchEventCommandCommandHandler> _logger;
    private readonly IMapper _mapper;
    public PatchEventCommandCommandHandler(IApplicationDbContext dbContext, ILogger<PatchEventCommandCommandHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(PatchEventCommandCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var found = _dbContext.Events.FirstOrDefault(x => x.Id == request.Id);

            if (found == null)
            {
                return new BaseResponse
                {
                    Status = false,
                    Message = "cannot find event",
                };
            }

            var entity = _mapper.Map(request, found);
            entity.UpdatedAt = DateTime.UtcNow;

            _dbContext.Events.Update(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new BaseResponse(true, "success");
        }
        catch (Exception ex)
        {
            _logger.LogCritical("[Exception]: {message}", ex.Message);
            return new BaseResponse(false, "error occurred");
        }
    }
}
