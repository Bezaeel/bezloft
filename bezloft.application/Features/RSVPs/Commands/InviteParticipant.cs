using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Common.Models;
using bezloft.core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace bezloft.application.Features.RSVPs.Commands
{
    public class InviteDTO
    {
        public Guid userId { get; set; }
    }
    public class InviteParticipantCommand : IRequest<BaseResponse>
    {
        public Guid UserId { get; set; }
        public Guid EventId { get; set; }
    }

    public class InviteParticipantHandler : IRequestHandler<InviteParticipantCommand, BaseResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly ILogger<InviteParticipantHandler> _logger;
        private readonly IMapper _mapper;

        public InviteParticipantHandler(IApplicationDbContext dbContext, ILogger<InviteParticipantHandler> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<BaseResponse> Handle(InviteParticipantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = _mapper.Map<RSVP>(request);
                
                _dbContext.RSVPs.Add(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                // send notification

                return new BaseResponse(true, "success");
            }
            catch (Exception ex)
            {
                _logger.LogCritical("[Exception]: {message}", ex.Message);
                return new BaseResponse(false, "error occurred");
            }
        }
    }
}
