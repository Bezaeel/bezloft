using AutoMapper;
using bezloft.application.Common.Interfaces;
using bezloft.application.Features.Events.Queries;
using bezloft.core.Entities;
using Bezsoft.Application.Common.Mappings;
using Microsoft.Extensions.Logging;
using Moq;

namespace bezloft.tests
{
    public class EventsUnitTest
    {
        private readonly IMapper mapper;
        private CancellationTokenSource _cts;

        public EventsUnitTest()
        {
            _cts = new CancellationTokenSource();
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetEventDetails_ShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<GetEventDetailsHandler>>();

            using var _dbContext = new SetUp().dbContext;
            var events = new SetUp().seedEvents(_dbContext);

            var query = new GetEventDetailsQuery
            {
                Id = events[0].Id
            };

            var _ = new GetEventDetailsHandler(_dbContext, loggerMock.Object, mapper);
            var expected = await _.Handle(query, _cts.Token);

            Assert.NotNull(expected.Data);

        }

        [Fact]
        public async Task GetEventParticipants_ShouldReturnSuccess()
        {
            var loggerMock = new Mock<ILogger<GetEventParticipantsHandler>>();

            using var _dbContext = new SetUp().dbContext;
            var events = new SetUp().seedRSVPs(_dbContext);

            var query = new GetEventParticipantsQuery
            {
                Id = events[0].EventId
            };

            var _ = new GetEventParticipantsHandler(_dbContext, loggerMock.Object, mapper);
            var expected = await _.Handle(query, _cts.Token);

            Assert.NotNull(expected.Data);

        }
    }
}