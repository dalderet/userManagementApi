namespace UserManagement.UnitTests
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserManagement.DataAccessLayer;
    using UserManagement.Model.Api;
    using Xunit;

    public class RepositoryTest
    {
        private readonly InMemoryRepository<User> _repository;

        public RepositoryTest()
        {
            _repository = new InMemoryRepository<User>();
        }

        [Fact]
        public async Task CreateUserInRepository()
        {
            var user = await UserMocks.UserMock();

            var created = await _repository.CreateItemAsync(user);
            Assert.True(created);

            var savedUser = await _repository.GetItemAsync(user.Id);
            Assert.Equal(user, savedUser);
        }

        [Fact]
        public async Task GetNotExistingUserInRepository()
        {
            var user = await UserMocks.UserMock();

            var created = await _repository.CreateItemAsync(user);
            Assert.True(created);

            var savedUser = await _repository.GetItemAsync(user.Id + 1, true);
            Assert.True(savedUser.Id == 0);

            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _repository.GetItemAsync(user.Id + 1, false));
        }

        [Fact]
        public async Task GetAllExistingUsersInRepository()
        {
            var users = (await UserMocks.UsersMock()).OrderBy(i => i.Id).ToList();

            await _repository.CreateItemsAsync(users);

            var savedUsers = (await _repository.GetItemsAsync(i => i.Id != 0)).OrderBy(i => i.Id).ToList();

            Assert.Equal(users, savedUsers);

            var serializedUsers = JsonConvert.SerializeObject(users);
            var serializedSavedUsers = JsonConvert.SerializeObject(savedUsers);

            Assert.Equal(serializedUsers, serializedSavedUsers);
        }

        [Fact]
        public async Task DeleteUser()
        {
            var users = (await UserMocks.UsersMock()).OrderBy(i => i.Id).ToList();

            await _repository.CreateItemsAsync(users);

            var savedUsers = (await _repository.GetItemsAsync(i => i.Id != 0)).OrderBy(i => i.Id).ToList();

            Assert.Equal(users, savedUsers);

            await _repository.DeleteItemAsync(10);

            var newSavedUsers = (await _repository.GetItemsAsync(i => i.Id != 0)).OrderBy(i => i.Id).ToList();

            Assert.True(savedUsers.Count == newSavedUsers.Count + 1);
        }

        [Fact]
        public async Task UpdateUser()
        {
            var users = (await UserMocks.UsersMock()).OrderBy(i => i.Id).ToList();

            await _repository.CreateItemsAsync(users);

            var savedUsers = (await _repository.GetItemsAsync(i => i.Id != 0)).OrderBy(i => i.Id).ToList();

            Assert.Equal(users, savedUsers);

            var myUser = await UserMocks.UserMock();

            await _repository.UpdateItemAsync(10, myUser);

            var mySavedUser = await _repository.GetItemAsync(10);

            Assert.Equal(myUser.Name, mySavedUser.Name);
        }
    }
}
