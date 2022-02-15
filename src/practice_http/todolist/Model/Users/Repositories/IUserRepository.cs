namespace Model.Users.Repositories
{
    using System.Threading;
    using System.Threading.Tasks;

    public interface IUserRepository
    {
        Task<User> GetAsync(string login);

        Task<User> CreateAsync(UserCreationInfo creationInfo, CancellationToken cancellationToken);
    }
}
