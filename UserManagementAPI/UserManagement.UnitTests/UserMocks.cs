namespace UserManagement.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UserManagement.Model.Api;

    public static class UserMocks
    {
        #region Mocks

        public static Task<User> UserMock()
        {
            return Task.FromResult(new User
            {
                Id = 50,
                Name = "Daniel Alderete Merino",
                Birthdate = new DateTime(1990, 12, 30)
            });
        }

        public static Task<List<User>> UsersMock()
        {
            return Task.FromResult(new List<User>
            {
            new User
            {
                Id = 1,
                Name = "Homer Simpson",
                Birthdate = new DateTime(1980, 12, 30)
            },
            new User
            {
                Id = 7,
                Name = "Moe Szyslak",
                Birthdate = new DateTime(1976, 4, 5)
            },
            new User
            {
                Id = 3,
                Name = "Barney Gumble",
                Birthdate = new DateTime(1980, 2, 8)
            },
            new User
            {
                Id = 4,
                Name = "Lenny Leonard",
                Birthdate = new DateTime(1978, 2, 24)
            },
            new User
            {
                Id = 10,
                Name = "Carl Carlson",
                Birthdate = new DateTime(1979, 10, 30)
            }
            });
        }

        #endregion
    }
}
