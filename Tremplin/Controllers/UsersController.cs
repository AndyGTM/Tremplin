using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data;
using Tremplin.Models;

namespace Tremplin.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        /// <summary>
        /// Gestionnaire d'utilisateur.
        /// </summary>
        private UserManager<User> UserManager { get; init; }

        /// <summary>
        /// Gestionnaire de sécurité.
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
        /// Permet d'accéder à la vue permettant de créer un utilisateur.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Permet de créer un utilisateur.
        /// </summary>
        /// <param name="userRegisterViewModel">User information</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterViewModel userRegisterViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
                result = this.View(userRegisterViewModel);
            else
            {
                // User creation
                User user = new()
                {
                    UserName = userRegisterViewModel.UserName,
                    Password = userRegisterViewModel.Password,
                    Email = userRegisterViewModel.Email
                };

                IdentityResult resultCreate = await this.UserManager.CreateAsync(user, userRegisterViewModel.Password);

                // User created ?
                if (!resultCreate.Succeeded)
                {
                    foreach (IdentityError item in resultCreate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(userRegisterViewModel);
                }
                else
                    result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }

        /// <summary>
        /// Permet d'accéder au menu permettant de modifier les informations d'un utilisateur
        /// </summary>
        [HttpGet]
        public IActionResult UpdateMenu()
        {
            return View();
        }

        /// <summary>
        /// Permet d'accéder à la vue permettant de modifier l'identifiant d'un utilisateur
        /// </summary>
        [HttpGet]
        public IActionResult UserNameUpdate()
        {
            return View();
        }

        /// <summary>
        /// Permet de modifier l'identifiant d'un utilisateur
        /// </summary>
        /// <param name="userNameUpdateViewModel">UserName information</param>
        [HttpPost]
        public async Task<IActionResult> UserNameUpdate(UserNameUpdateViewModel userNameUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
                result = this.View(userNameUpdateViewModel);
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
                    result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }

        /// <summary>
        /// Permet d'accéder à la vue permettant de modifier le mot de passe d'un utilisateur
        /// </summary>
        [HttpGet]
        public IActionResult PasswordUpdate()
        {
            return View();
        }

        /// <summary>
        /// Permet de modifier le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="passwordUpdateViewModel">UserName information</param>
        [HttpPost]
        public async Task<IActionResult> PasswordUpdate(PasswordUpdateViewModel passwordUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
                result = this.View(passwordUpdateViewModel);
            else
            {
                // Password update
                User user = await UserManager.GetUserAsync(User);
                string hashPassword = UserManager.PasswordHasher.HashPassword(user, passwordUpdateViewModel.Password);
                user.Password = hashPassword;
                IdentityResult resultUpdate = await this.UserManager.UpdateAsync(user);

                // Password updated ?
                if (!resultUpdate.Succeeded)
                {
                    foreach (IdentityError item in resultUpdate.Errors)
                        this.ModelState.AddModelError(string.Empty, item.Description);
                    result = this.View(passwordUpdateViewModel);
                }
                else
                    result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }

        /// <summary>
        /// Permet d'accéder au menu permettant de modifier l'email d'un utilisateur
        /// </summary>
        [HttpGet]
        public IActionResult EmailUpdate()
        {
            return View();
        }

        /// <summary>
        /// Permet de modifier l'email d'un utilisateur
        /// </summary>
        /// <param name="emailUpdateViewModel">Email information</param>
        [HttpPost]
        public async Task<IActionResult> EmailUpdate(EmailUpdateViewModel emailUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
                result = this.View(emailUpdateViewModel);
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
                    result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return this.View();
        }

        /// <summary>
        /// Permet d'authentifier un utilisateur.
        /// </summary>
        /// <param name="userLoginViewModel">User credentials</param>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginViewModel userLoginViewModel)
        {
            IActionResult result;
            if (!this.ModelState.IsValid)
                result = this.View(userLoginViewModel);
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
        /// Permet de déconnecter un utilisateur.
        /// </summary>
        [AllowAnonymous]
        public ActionResult Logout()
        {
            this.LoginManager.SignOutAsync();

            return this.RedirectToAction(nameof(HomeController.Index), "Home");
        }

        /// <summary>
        /// Méthode accessible aux utilisateurs authentifiés.
        /// </summary>
        public IActionResult AccessAllowed()
        {
            return this.View();
        }

        /// <summary>
        /// Méthode accessible à tous utilisateurs.
        /// </summary>
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return this.View();
        }
    }
}