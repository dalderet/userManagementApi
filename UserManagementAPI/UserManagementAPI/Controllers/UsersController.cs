namespace UserManagementAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using UserManagement.Domain.IManagers.Api;
    using UserManagement.Model.Api;

    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserManagementService _userManagementService;

        public UsersController(IUserManagementService userManagementService)
        {
            _userManagementService = userManagementService;
        }

        // POST api/users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var added = await _userManagementService.CreateUser(user);

            if (added)
                return Ok("User created");
            else
                return Conflict("User already existing");
        }

        // GET api/users/x where x is int
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManagementService.GetUser(id);

            if (user.Id == 0)
                return NotFound("User not found");

            return Ok(user);
        }

        // GET api/users/all
        [HttpGet("all")]
        public async Task<IActionResult> GetUsers()
        {
            var allUsers = await _userManagementService.GetAllUsers();
            return Ok(allUsers);
        }

        // PUT api/users
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _userManagementService.UpdateUser(user);
            return Ok("User updated or created if not existing");
        }

        // DELETE api/users/x where x is int
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userManagementService.DeleteUser(id);
            return Ok("User deleted");
        }
    }
}
