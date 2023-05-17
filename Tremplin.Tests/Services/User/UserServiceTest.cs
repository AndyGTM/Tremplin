using Tremplin.IServices;
using Tremplin.Services;

namespace Tremplin.Tests.Services.User
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly IUserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService();
        }

        #region CRUD Users

        [TestMethod("Return a user from the \"CreateUser\" service")]
        public void Create_User_ReturnUserFromService()
        {
            Data.Entity.User userMock = new()
            {
                Id = 5,
                UserName = "Denis",
                Email = "denis.rjd@gmail.com",
                Password = ""
            };

            Data.Entity.User userResult = _userService.CreateUser(userMock.UserName, userMock.Password, userMock.Email);

            Assert.IsNotNull(userResult);
        }

        #endregion CRUD Users
    }
}