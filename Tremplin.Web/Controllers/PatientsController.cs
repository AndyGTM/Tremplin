using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            IQueryable<Patient> patients = _patientService.GetPatients(user.UserName);

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

            PatientListViewModel patientListViewModel = new()
            {
                Patients = await patients.ToListAsync()
            };

            foreach (Patient patient in patientListViewModel.Patients)
            {
                // Adding blank spaces for displaying the social security number
                patient.SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patient.SocialSecurityNumber);

                // Check if user is the creator of the patient
                if (patient.CreatedBy == user.UserName)
                {
                    patient.UserIsCreator = true;
                }
                else
                {
                    patient.UserIsCreator = false;
                }
            }

            return View(patientListViewModel);
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
        /// <param name="patientCreationViewModel">Patient information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreationViewModel patientCreationViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(patientCreationViewModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);

                // Patient creation
                _patientService.CreatePatient
                    (
                        patientCreationViewModel.SocialSecurityNumber,
                        patientCreationViewModel.LastName,
                        patientCreationViewModel.FirstName,
                        patientCreationViewModel.BirthDate,
                        patientCreationViewModel.BloodGroup,
                        patientCreationViewModel.Sex,
                        patientCreationViewModel.SharedSheet,
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

            PatientUpdateViewModel patientUpdateViewModel = new()
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

            return View(patientUpdateViewModel);
        }

        /// <summary>
        /// Allows to update a patient
        /// </summary>
        /// <param name="patientUpdateViewModel">Patient information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PatientUpdateViewModel patientUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(patientUpdateViewModel);
            }
            else
            {
                // Patient update
                PatientModel patientModel = _patientService.GetPatientById(id);

                _patientService.UpdatePatient
                    (
                        patientModel,
                        patientUpdateViewModel.SocialSecurityNumber,
                        patientUpdateViewModel.LastName,
                        patientUpdateViewModel.FirstName,
                        patientUpdateViewModel.BirthDate,
                        patientUpdateViewModel.BloodGroup,
                        patientUpdateViewModel.Sex,
                        patientUpdateViewModel.SharedSheet
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

            PatientDeleteViewModel patientDeleteViewModel = new()
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

            return View(patientDeleteViewModel);
        }

        /// <summary>
        /// Allows to delete a patient
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, PatientDeleteViewModel patientDeleteViewModel)
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