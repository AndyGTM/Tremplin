using Tremplin.Data.Entity;
using Tremplin.IServices;

namespace Tremplin.Services
{
    public class UserService : IUserService
    {
        #region CRUD Users

        public User CreateUser(string userName, string password, string email)
        {
            // User creation
            User user = new()
            {
                UserName = userName,
                Password = password,
                Email = email
            };

            return user;
        }

        #endregion CRUD Users
    }
}