using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data.Entity;
using Tremplin.IServices;
using Tremplin.Models.UserViewModels;

namespace Tremplin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        /// <summary>
        /// User manager
        /// </summary>
        private UserManager<User> UserManager { get; init; }

        /// <summary>
        /// Security manager
        /// </summary>
        private SignInManager<User> LoginManager { get; init; }

        private readonly IUserService _userService;

        public UsersController(UserManager<User> aUserManager, SignInManager<User> aLoginManager, IUserService userService)
        {
            this.UserManager = aUserManager;
            this.LoginManager = aLoginManager;
            _userService = userService;
        }

        /// <summary>
        /// Provides access to the view for creating a user
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Allows to create a user
        /// </summary>
        /// <param name="userRegistrationModel">User information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel userRegistrationModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(userRegistrationModel);
            }
            else
            {
                // User creation
                User user = _userService.CreateUser
                    (
                        userRegistrationModel.UserName,
                        userRegistrationModel.Password,
                        userRegistrationModel.Email
                    );

                IdentityResult resultCreate = await this.UserManager.CreateAsync(user, userRegistrationModel.Password);

                // User created ?
                if (!resultCreate.Succeeded)
                {
                    foreach (IdentityError item in resultCreate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(userRegistrationModel);
                }
                else
                    result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }

        /// <summary>
        /// Provides access to the menu for updating an user's information
        /// </summary>
        [HttpGet]
        public IActionResult UpdateMenu()
        {
            return View();
        }

        /// <summary>
        /// Provides access to the view for updating an user's name
        /// </summary>
        [HttpGet]
        public IActionResult UserNameUpdate()
        {
            return View();
        }

        /// <summary>
        /// Allows to update an user's name
        /// </summary>
        /// <param name="userNameUpdateModel">UserName information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserNameUpdate(UserNameUpdateModel userNameUpdateModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(userNameUpdateModel);
            }
            else
            {
                // UserName update
                User user = await UserManager.GetUserAsync(User);
                user.UserName = userNameUpdateModel.UserName;
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // UserName updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(userNameUpdateModel);
                }
                else
                    result = this.RedirectToAction(nameof(this.UpdateSucceeded));
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for updating an user's password
        /// </summary>
        [HttpGet]
        public IActionResult PasswordUpdate()
        {
            return View();
        }

        /// <summary>
        /// Allows to update an user's password
        /// </summary>
        /// <param name="passwordUpdateModel">Password information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordUpdate(PasswordUpdateModel passwordUpdateModel)
        {
            IActionResult result;

            // First input data validation
            if (!this.ModelState.IsValid)
            {
                result = this.View(passwordUpdateModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);

                #region Password validators

                IList<IPasswordValidator<User>> validators = UserManager.PasswordValidators;

                foreach (IPasswordValidator<User> validator in validators)
                {
                    IdentityResult resultValidator = await validator.ValidateAsync(UserManager, user, passwordUpdateModel.Password);

                    if (!resultValidator.Succeeded)
                    {
                        foreach (IdentityError error in resultValidator.Errors)
                        {
                            this.ModelState.AddModelError(string.Empty, error.Description);
                        }

                        result = this.View(passwordUpdateModel);
                        return result;
                    }
                }

                #endregion Password validators

                // Password update
                user.Password = UserManager.PasswordHasher.HashPassword(user, passwordUpdateModel.Password);
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // Password updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(passwordUpdateModel);
                }
                else
                    result = this.RedirectToAction(nameof(this.UpdateSucceeded));
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for updating an user's email
        /// </summary>
        [HttpGet]
        public IActionResult EmailUpdate()
        {
            return View();
        }

        /// <summary>
        /// Allows to update an user's email
        /// </summary>
        /// <param name="emailUpdateModel">Email information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailUpdate(EmailUpdateModel emailUpdateModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(emailUpdateModel);
            }
            else
            {
                // Email update
                User user = await UserManager.GetUserAsync(User);
                user.Email = emailUpdateModel.Email;
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // Email updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(emailUpdateModel);
                }
                else
                    result = this.RedirectToAction(nameof(this.UpdateSucceeded));
            }

            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        /// <summary>
        /// Provides access to the view for the user login
        /// </summary>
        public IActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        /// Allows a user to login
        /// </summary>
        /// <param name="userLoginModel">User credentials</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            IActionResult result;
            if (!this.ModelState.IsValid)
            {
                result = this.View(userLoginModel);
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult resultLogin = await this.LoginManager.PasswordSignInAsync(userLoginModel.UserName,
                    userLoginModel.Password, userLoginModel.IsRememberMe, false);

                if (!resultLogin.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, "Identifiant ou mot de passe non correct ");
                    result = this.View(userLoginModel);
                }
                else
                    result = this.RedirectToAction(resultLogin.Succeeded ? nameof(this.AccessAllowed) : nameof(this.AccessDenied));
            }

            return result;
        }

        /// <summary>
        /// Allows a user to logout
        /// </summary>
        [AllowAnonymous]
        public ActionResult Logout()
        {
            this.LoginManager.SignOutAsync();

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Method accessible to authenticated users
        /// </summary>
        public IActionResult AccessAllowed()
        {
            return this.View();
        }

        /// <summary>
        /// Method accessible to all users
        /// </summary>
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        /// <summary>
        /// Provides access to the view when an update succeeds
        /// </summary>
        public IActionResult UpdateSucceeded()
        {
            return this.View();
        }
    }
}