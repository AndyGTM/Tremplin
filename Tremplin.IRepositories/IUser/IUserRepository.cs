using Tremplin.Data;

namespace Tremplin.IRepositories.IUser
{
    public interface IUserRepository<T> where T : User
    {
        T GetUserById(int userId, CancellationToken cancellationToken);

        T GetUserByName(string normalizedUserName, CancellationToken cancellationToken);

        void CreateUser(T user, CancellationToken cancellationToken);

        void UpdateUser(T user, CancellationToken cancellationToken);
    }
}