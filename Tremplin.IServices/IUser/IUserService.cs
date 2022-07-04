using Tremplin.Data.Entity;

namespace Tremplin.IServices
{
    public interface IUserService
    {
        #region CRUD Users

        User CreateUser(string userName, string password, string email);

        #endregion CRUD Users
    }
}
