using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Core.Helpers;
using Tremplin.Data;
using Tremplin.IServices.IPatient;
using Tremplin.Models.Patient;
using Tremplin.Models.PatientViewModels;

namespace Tremplin.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        /// <summary>
        /// User manager
        /// </summary>
        private UserManager<User> UserManager { get; init; }

        private readonly IPatientService _patientService;

        public PatientsController(UserManager<User> aUserManager, IPatientService patientService)
        {
            this.UserManager = aUserManager;
            _patientService = patientService;
        }

        /// <summary>
        /// Provides access to the view for searching and listing patients
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index(string searchLastName, string searchFirstName, string searchSocialSecurityNumber, DateTime? searchBirthDate)
        {
            User user = await UserManager.GetUserAsync(User);

            IEnumerable<PatientModel> patients = _patientService.GetPatients(user.UserName);

            if (!string.IsNullOrEmpty(searchLastName))
            {
                patients = patients.Where(s => s.LastName!.Contains(searchLastName));
            }

            if (!string.IsNullOrEmpty(searchFirstName))
            {
                patients = patients.Where(s => s.FirstName!.Contains(searchFirstName));
            }

            if (!string.IsNullOrEmpty(searchSocialSecurityNumber))
            {
                // Allow user to search social security number by entering blank spaces
                if (searchSocialSecurityNumber.Contains(' '))
                {
                    searchSocialSecurityNumber = SocialSecurityNumberHelper.RemoveBlankSpacesInSocialSecurityNumber(searchSocialSecurityNumber);

                    patients = patients.Where(s => s.SocialSecurityNumber!.Contains(searchSocialSecurityNumber));
                }

                else
                {
                    patients = patients.Where(s => s.SocialSecurityNumber!.Contains(searchSocialSecurityNumber));
                }
            }

            if (searchBirthDate.HasValue)
            {
                patients = patients.Where(s => s.BirthDate!.Equals(searchBirthDate));
            }

            PatientListModel patientListModel = new()
            {
                Patients = patients.ToList()
            };

            foreach (PatientModel patientModel in patientListModel.Patients)
            {
                // Adding blank spaces for displaying the social security number
                patientModel.SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber);

                // Check if user is the creator of the patient
                if (patientModel.CreatedBy == user.UserName)
                {
                    patientModel.UserIsCreator = true;
                }
                else
                {
                    patientModel.UserIsCreator = false;
                }
            }

            return View(patientListModel);
        }

        /// <summary>
        /// Provides access to the view for creating a patient
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Allows to create a patient
        /// </summary>
        /// <param name="patientCreationModel">Patient information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreationModel patientCreationModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(patientCreationModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);

                // Patient creation
                _patientService.CreatePatient
                    (
                        patientCreationModel.SocialSecurityNumber,
                        patientCreationModel.LastName,
                        patientCreationModel.FirstName,
                        patientCreationModel.BirthDate,
                        patientCreationModel.BloodGroup,
                        patientCreationModel.Sex,
                        patientCreationModel.SharedSheet,
                        user.UserName
                    );

                result = this.RedirectToAction(nameof(this.Index));
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for updating a patient
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            PatientUpdateModel patientUpdateModel = new()
            {
                Id = patientModel.Id,

                // Adding blank spaces for displaying the social security number
                SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber),

                LastName = patientModel.LastName,
                FirstName = patientModel.FirstName,
                BirthDate = patientModel.BirthDate,
                BloodGroup = patientModel.BloodGroup,
                Sex = patientModel.Sex,
                SharedSheet = patientModel.SharedSheet
            };

            return View(patientUpdateModel);
        }

        /// <summary>
        /// Allows to update a patient
        /// </summary>
        /// <param name="patientUpdateModel">Patient information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PatientUpdateModel patientUpdateModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(patientUpdateModel);
            }
            else
            {
                // Patient update
                PatientModel patientModel = _patientService.GetPatientById(id);

                _patientService.UpdatePatient
                    (
                        patientModel,
                        patientUpdateModel.SocialSecurityNumber,
                        patientUpdateModel.LastName,
                        patientUpdateModel.FirstName,
                        patientUpdateModel.BirthDate,
                        patientUpdateModel.BloodGroup,
                        patientUpdateModel.Sex,
                        patientUpdateModel.SharedSheet
                    );

                result = this.RedirectToAction(nameof(this.Index));
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for deleting a patient
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            PatientDeleteModel patientDeleteModel = new()
            {
                Id = patientModel.Id,

                // Adding blank spaces for displaying the social security number
                SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber),

                LastName = patientModel.LastName,
                FirstName = patientModel.FirstName,
                BirthDate = patientModel.BirthDate,
                BloodGroup = patientModel.BloodGroup,
                Sex = patientModel.Sex,
                SharedSheet = patientModel.SharedSheet
            };

            return View(patientDeleteModel);
        }

        /// <summary>
        /// Allows to delete a patient
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, PatientDeleteModel patientDeleteModel)
        {
            IActionResult result;

            // Patient delete
            PatientModel patientModel = _patientService.GetPatientById(id);

            _patientService.DeletePatient(patientModel);

            result = this.RedirectToAction(nameof(this.Index));

            return result;
        }
    }
}