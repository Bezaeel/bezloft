using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.core.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.Profile.Commands;

public class RegisterUserDTO
{
    public string name { get; set; }
    public string email { get; set; }
}

public class RegisterUserCommand : IRequest<BaseResponse>
{
    public string Name { get; set; }
    public string Email { get; set; }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("email field is required").EmailAddress().WithMessage("Invalid email");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("name is required");
    }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly IMapper _mapper;
    public RegisterUserCommandHandler(IApplicationDbContext dbContext, ILogger<RegisterUserCommandHandler> logger, IMapper mapper)
    {
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var entity = _mapper.Map<User>(request);

            _dbContext.Users.Add(entity);
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
