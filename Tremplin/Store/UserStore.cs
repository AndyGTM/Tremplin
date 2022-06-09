using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tremplin.Data;

namespace Tremplin.Store
{
    public class UserStore : IUserStore<User>, IUserPasswordStore<User>
    {
        private DataContext DataContext { get; init; }

        public UserStore(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
        {
            // Adding the user to the data context
            DataContext.Add(user);

            // Persistance de l'ajout de l'utilisateur dans la base de données.
            await DataContext.SaveChangesAsync(cancellationToken);

            // Return
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            // Updating the user to the data context
            DataContext.Update(user);

            // Persistance de la modification de l'utilisateur dans la base de données.
            await DataContext.SaveChangesAsync(cancellationToken);

            // Return
            return await Task.FromResult(IdentityResult.Success);
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
        {
            // Deleting the user from the data context
            DataContext.Remove(user);

            // Persistance de la suppression de l'utilisateur dans la base de données.
            int result = await DataContext.SaveChangesAsync(cancellationToken);

            // Return
            return await Task.FromResult(result == 1 ? IdentityResult.Success : IdentityResult.Failed());
        }

        public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            // Recherche d’un utilisateur à partir de son identifiant.
            return int.TryParse(userId, out int id) ?
                await DataContext.Users.FindAsync(id) :
                await Task.FromResult((User)null);
        }

        public async Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            // Recherche d’un utilisateur à partir de son nom d’utilisateur.
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
