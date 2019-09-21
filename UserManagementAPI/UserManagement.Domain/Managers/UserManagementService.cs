namespace UserManagement.Domain.Managers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using UserManagement.DataAccessLayer;
    using UserManagement.Domain.IManagers.Api;
    using UserManagement.Model.Api;

    public class UserManagementService : IUserManagementService
    {
        private readonly IRepository<User> _userRepository;

        public UserManagementService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(User user)
        {
            return await _userRepository.CreateItemAsync(user);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _userRepository.GetItemAsync(id, true);
            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return (await _userRepository.GetItemsAsync(i => i.Id != 0)).OrderBy(i => i.Id).ToList();
        }

        public async Task UpdateUser(User user)
        {
            await _userRepository.UpsertItemAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteItemAsync(id);
        }
    }
}
