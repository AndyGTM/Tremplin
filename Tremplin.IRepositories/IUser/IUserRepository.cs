using Tremplin.Data;

namespace Tremplin.IRepositories.IUser
{
    public interface IUserRepository<T> where T : User
    {
        void CreateUser(T user, CancellationToken cancellationToken);
    }
}