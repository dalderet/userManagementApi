namespace UserManagement.UnitTests
{
    using Moq;
    using System.Linq;
    using System.Threading.Tasks;
    using UserManagement.DataAccessLayer;
    using UserManagement.Domain.Managers;
    using UserManagement.Model.Api;
    using Xunit;

    public class UserManagementServiceTest
    {
        private readonly Mock<IRepository<User>> _userMock;

        public UserManagementServiceTest()
        {
            _userMock = new Mock<IRepository<User>>();
        }

        [Fact]
        public async Task GetExistingUser()
        {
            _userMock.Setup(x => x.GetItemAsync(1, false)).Returns(async () => await UserMocks.UserMock());
            var service = new UserManagementService(_userMock.Object);
            var user = await service.GetUser(1);
            Assert.Equal("Daniel Alderete Merino", user.Name);
        }

        [Fact]
        public async Task GetNonExistingUser()
        {
            _userMock.Setup(x => x.GetItemAsync(2, true)).Returns(async () => await UserMocks.UserMock());
            var service = new UserManagementService(_userMock.Object);
            var user = await service.GetUser(1);
            Assert.True(user is null);
        }

        [Fact]
        public async Task GetAllUsers()
        {
            _userMock.Setup(x => x.GetItemsAsync(i => i.Id != 0)).Returns(async () => await UserMocks.UsersMock());
            var service = new UserManagementService(_userMock.Object);
            var users = await service.GetAllUsers();
            Assert.True(users.Count == 5);
            Assert.True(users.FirstOrDefault(i => i.Id == 2) is null);
            Assert.True(users.OrderByDescending(i => i.Id).FirstOrDefault().Id == 10);
        }

        [Fact]
        public async Task CreateUser()
        {
            var user = await UserMocks.UserMock();
            _userMock.Setup(x => x.CreateItemAsync(user)).Returns(Task.FromResult(true));
            var service = new UserManagementService(_userMock.Object);
            bool created = await service.CreateUser(user);
            Assert.True(created);
        }
    }
}
