using bezloft.application.Common.Interfaces;
using bezloft.core.Entities;
using Moq;

namespace bezloft.tests
{
    public class ProfileUnitTest
    {
        [Fact]
        public async Task RegisterUser_NewUser_ShouldReturnSuccess()
        {
            var entity = new User
            {
                Name = "Test",
                Email = "talabi@mail.com"
            };
            using var _dbContext = new SetUp().dbContext;
            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();

            Assert.NotNull(_dbContext.Users);

        }
    }
}