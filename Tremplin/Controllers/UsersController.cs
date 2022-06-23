using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data;
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

        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController(UserManager<User> aUserManager, SignInManager<User> aLoginManager)
        {
            this.UserManager = aUserManager;
            this.LoginManager = aLoginManager;
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
        /// <param name="userRegistrationViewModel">User information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationViewModel userRegistrationViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(userRegistrationViewModel);
            }
            else
            {
                // User creation
                User user = new()
                {
                    UserName = userRegistrationViewModel.UserName,
                    Password = userRegistrationViewModel.Password,
                    Email = userRegistrationViewModel.Email
                };

                IdentityResult resultCreate = await this.UserManager.CreateAsync(user, userRegistrationViewModel.Password);

                // User created ?
                if (!resultCreate.Succeeded)
                {
                    foreach (IdentityError item in resultCreate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(userRegistrationViewModel);
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
        /// <param name="userNameUpdateViewModel">UserName information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserNameUpdate(UserNameUpdateViewModel userNameUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(userNameUpdateViewModel);
            }
            else
            {
                // UserName update
                User user = await UserManager.GetUserAsync(User);
                user.UserName = userNameUpdateViewModel.UserName;
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // UserName updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(userNameUpdateViewModel);
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
        /// <param name="passwordUpdateViewModel">UserName information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordUpdate(PasswordUpdateViewModel passwordUpdateViewModel)
        {
            IActionResult result;

            // First input data validation
            if (!this.ModelState.IsValid)
            {
                result = this.View(passwordUpdateViewModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);

                #region Password validators

                IList<IPasswordValidator<User>> validators = UserManager.PasswordValidators;

                foreach (IPasswordValidator<User> validator in validators)
                {
                    IdentityResult resultValidator = await validator.ValidateAsync(UserManager, user, passwordUpdateViewModel.Password);

                    if (!resultValidator.Succeeded)
                    {
                        foreach (IdentityError error in resultValidator.Errors)
                        {
                            this.ModelState.AddModelError(string.Empty, error.Description);
                        }

                        result = this.View(passwordUpdateViewModel);
                        return result;
                    }
                }

                #endregion Password validators

                // Password update
                user.Password = UserManager.PasswordHasher.HashPassword(user, passwordUpdateViewModel.Password);
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // Password updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(passwordUpdateViewModel);
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
        /// <param name="emailUpdateViewModel">Email information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailUpdate(EmailUpdateViewModel emailUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(emailUpdateViewModel);
            }
            else
            {
                // Email update
                User user = await UserManager.GetUserAsync(User);
                user.Email = emailUpdateViewModel.Email;
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // Email updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(emailUpdateViewModel);
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
        /// <param name="userLoginViewModel">User credentials</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            IActionResult result;
            if (!this.ModelState.IsValid)
            {
                result = this.View(userLoginViewModel);
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult resultLogin = await this.LoginManager.PasswordSignInAsync(userLoginViewModel.UserName,
                    userLoginViewModel.Password, userLoginViewModel.IsRememberMe, false);

                if (!resultLogin.Succeeded)
                {
                    this.ModelState.AddModelError(string.Empty, "Identifiant ou mot de passe non correct ");
                    result = this.View(userLoginViewModel);
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