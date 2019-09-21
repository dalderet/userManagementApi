namespace UserManagement.Domain.IManagers.Api
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManagement.Model.Api;

    public interface IUserManagementService
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUser(int id);
        Task<List<User>> GetAllUsers();
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
