using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.core.Entities;
using bezloft.core.Enums;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Events.Commands;

public class CreateEventDTO
{
    public string name { get; set; }
    //public Guid contactPersonId { get; set; }
    public string description { get; set; }
    public int limit { get; set; }
    public EventVisibility visibility { get; set; }
    public DateTime start { get; set; }
    public DateTime end { get; set; }
}

public class CreateEventCommand : IRequest<BaseResponse>
{
    public string Name { get; set; }
    public Guid ContactPersonId { get; set; }
    public EventVisibility Visibility { get; set; }
    public int Limit { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("name is required");
    }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, BaseResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<CreateEventCommandHandler> _logger;
    private readonly IMapper _mapper;
    public CreateEventCommandHandler(IApplicationDbContext dbContext, ILogger<CreateEventCommandHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<Event>(request);

            _dbContext.Events.Add(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new BaseResponse(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogCritical("[Exception]: {message}", ex.Message);
            return new BaseResponse(false, "Error occurred");
        }
    }
}
