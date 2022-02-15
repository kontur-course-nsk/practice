namespace Model.Users.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class UserRepository : IUserRepository
    {
        private readonly IList<User> users;

        public UserRepository()
        {
            this.users = new List<User>();
        }

        public Task<User> GetAsync(string login)
        {
            var result = users.FirstOrDefault(user => user.Login == login);

            if (result is null)
            {
                throw new UserNotFoundException(login);
            }

            return Task.FromResult(result);
        }

        public Task<User> CreateAsync(UserCreationInfo creationInfo, CancellationToken token)
        {
            if (creationInfo == null)
            {
                throw new ArgumentNullException(nameof(creationInfo));
            }

            token.ThrowIfCancellationRequested();

            var countUsersWithSameLogin = users.Count(usr => usr.Login == creationInfo.Login);

            if (countUsersWithSameLogin > 0)
            {
                throw new UserDuplicationException(creationInfo.Login);
            }

            var user = new User
            {
                Login = creationInfo.Login,
                PasswordHash = creationInfo.PasswodHash,
                RegisteredAt = DateTime.Now,
            };

            users.Add(user);
            return Task.FromResult(user);
        }
    }
}
