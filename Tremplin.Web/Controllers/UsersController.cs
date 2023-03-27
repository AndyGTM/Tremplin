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
        private UserManager<User> UserManager { get; init; }

        private SignInManager<User> LoginManager { get; init; }

        private readonly IUserService _userService;

        public UsersController(UserManager<User> aUserManager, SignInManager<User> aLoginManager, IUserService userService)
        {
            UserManager = aUserManager;
            LoginManager = aLoginManager;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel userRegistrationModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(userRegistrationModel);
            }
            else
            {
                User user = _userService.CreateUser
                    (
                        userRegistrationModel.UserName,
                        userRegistrationModel.Password,
                        userRegistrationModel.Email
                    );

                IdentityResult resultCreate = await UserManager.CreateAsync(user, userRegistrationModel.Password);

                if (!resultCreate.Succeeded)
                {
                    foreach (IdentityError item in resultCreate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    result = View(userRegistrationModel);
                }
                else
                {
                    result = RedirectToAction(nameof(Index), "Home");
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult UpdateMenu()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UserNameUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserNameUpdate(UserNameUpdateModel userNameUpdateModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(userNameUpdateModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);
                user.UserName = userNameUpdateModel.UserName;
                IdentityResult resultUpdate = await UserManager.UpdateAsync(user);

                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    result = View(userNameUpdateModel);
                }
                else
                {
                    result = RedirectToAction(nameof(this.UpdateSucceeded));
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult PasswordUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PasswordUpdate(PasswordUpdateModel passwordUpdateModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(passwordUpdateModel);
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
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        result = View(passwordUpdateModel);
                        return result;
                    }
                }

                #endregion Password validators

                user.Password = UserManager.PasswordHasher.HashPassword(user, passwordUpdateModel.Password);
                IdentityResult resultUpdate = await UserManager.UpdateAsync(user);

                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    result = View(passwordUpdateModel);
                }
                else
                {
                    result = RedirectToAction(nameof(this.UpdateSucceeded));
                }
            }

            return result;
        }

        [HttpGet]
        public IActionResult EmailUpdate()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmailUpdate(EmailUpdateModel emailUpdateModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(emailUpdateModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);
                user.Email = emailUpdateModel.Email;
                IdentityResult resultUpdate = await UserManager.UpdateAsync(user);

                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }

                    result = View(emailUpdateModel);
                }
                else
                {
                    result = RedirectToAction(nameof(this.UpdateSucceeded));
                }
            }

            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel userLoginModel)
        {
            IActionResult result;
            if (!ModelState.IsValid)
            {
                result = View(userLoginModel);
            }
            else
            {
                Microsoft.AspNetCore.Identity.SignInResult resultLogin = await LoginManager.PasswordSignInAsync(userLoginModel.UserName,
                    userLoginModel.Password, userLoginModel.IsRememberMe, false);

                if (!resultLogin.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Identifiant ou mot de passe non correct ");
                    result = View(userLoginModel);
                }
                else
                    result = RedirectToAction(resultLogin.Succeeded ? nameof(this.AccessAllowed) : nameof(this.AccessDenied));
            }

            return result;
        }

        [AllowAnonymous]
        public ActionResult Logout()
        {
            LoginManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult AccessAllowed()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult UpdateSucceeded()
        {
            return View();
        }
    }
}