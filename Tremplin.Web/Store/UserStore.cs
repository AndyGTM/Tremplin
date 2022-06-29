using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.IRepositories.IUser;

namespace Tremplin.Store
{
    public class UserStore : IUserPasswordStore<User>
    {
        private DataContext DataContext { get; init; }

        private IUserRepository<User> _userRepository { get; set; }

        public UserStore(DataContext dataContext, IUserRepository<User> userRepository)
        {
            DataContext = dataContext;
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _userRepository.CreateUser(user, cancellationToken);

            // Return
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            _userRepository.UpdateUser(user, cancellationToken);

            // Return
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            // Deleting the user from the data context
            DataContext.Remove(user);

            // Persistence of deleting the user to the database
            int result = await DataContext.SaveChangesAsync(cancellationToken);

            // Return
            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            // Find an user by his Id
            return int.TryParse(userId, out int id) ?
                _userRepository.GetUserById(id, cancellationToken) :
                await Task.FromResult((User)null);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            // Find an user by his name
            return await DataContext.Users
                .SingleOrDefaultAsync(u => u.UserName.Equals(normalizedUserName.ToLower()), cancellationToken);
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

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult((object)null);
        }

        public Task SetPasswordHashAsync(User user, string password, CancellationToken cancellationToken)
        {
            user.Password = password;

            return Task.FromResult((object)null);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            return Task.FromResult(!string.IsNullOrWhiteSpace(user.Password));
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}