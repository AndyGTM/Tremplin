using Tremplin.Data;
using Tremplin.Data.Entity;
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

        public T GetUserById(int userId, CancellationToken cancellationToken)
        {
            return DataContext.Set<T>().Find(userId);
        }

        public T GetUserByName(string normalizedUserName, CancellationToken cancellationToken)
        {
            return DataContext.Set<T>().SingleOrDefault(u => u.UserName.Equals(normalizedUserName.ToLower()));
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DataContext?.Dispose();
        }
    }
}