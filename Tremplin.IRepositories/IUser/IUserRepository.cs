using Tremplin.Data.Entity.User;

namespace Tremplin.IRepositories.IUser
{
    public interface IUserRepository<T> : IDisposable where T : User
    {
        T GetUserById(int userId, CancellationToken cancellationToken);

        T GetUserByName(string normalizedUserName, CancellationToken cancellationToken);

        void CreateUser(T user, CancellationToken cancellationToken);

        void UpdateUser(T user, CancellationToken cancellationToken);
    }
}