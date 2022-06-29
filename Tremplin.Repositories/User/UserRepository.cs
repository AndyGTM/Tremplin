using Tremplin.Data;
using Tremplin.IRepositories.IUser;

namespace Tremplin.Repositories
{
    public class UserRepository<T> : IUserRepository<T> where T : User
    {
        private DataContext DataContext { get; init; }

        public UserRepository(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public void CreateUser(T user, CancellationToken cancellationToken)
        {
            DataContext.Add(user);

            DataContext.SaveChanges();
        }

        public void UpdateUser(T user, CancellationToken cancellationToken)
        {
            DataContext.Update(user);

            DataContext.SaveChanges();
        }
    }
}