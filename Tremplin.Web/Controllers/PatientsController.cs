using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Core.Helpers;
using Tremplin.Data.Entity;
using Tremplin.IServices;
using Tremplin.Models;
using Tremplin.Models.PatientViewModels;

namespace Tremplin.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private UserManager<User> UserManager { get; init; }

        private readonly IPatientService _patientService;

        public PatientsController(UserManager<User> aUserManager, IPatientService patientService)
        {
            UserManager = aUserManager;
            _patientService = patientService;
        }

        #region CRUD Patients

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
                patientModel.SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber);

                if (patientModel.CreatedBy == user.UserName)
                {
                    patientModel.UserIsCreatorOfPatientSheet = true;
                }
                else
                {
                    patientModel.UserIsCreatorOfPatientSheet = false;
                }
            }

            return View(patientListModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreationModel patientCreationModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(patientCreationModel);
            }
            else
            {
                User user = await UserManager.GetUserAsync(User);

                PatientModel patientModel = new()
                {
                    SocialSecurityNumber = patientCreationModel.SocialSecurityNumber,
                    LastName = patientCreationModel.LastName,
                    FirstName = patientCreationModel.FirstName,
                    BirthDate = patientCreationModel.BirthDate,
                    BloodGroup = patientCreationModel.BloodGroup,
                    Sex = patientCreationModel.Sex,
                    SharedSheet = patientCreationModel.SharedSheetWithOthersPractitioners,
                    CreatedBy = user.UserName
                };

                _patientService.CreatePatient(patientModel);

                result = RedirectToAction(nameof(this.Index));
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            PatientUpdateModel patientUpdateModel = new()
            {
                Id = patientModel.Id,
                SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber),
                LastName = patientModel.LastName,
                FirstName = patientModel.FirstName,
                BirthDate = patientModel.BirthDate,
                BloodGroup = patientModel.BloodGroup,
                Sex = patientModel.Sex,
                SharedSheetWithOthersPractitioners = patientModel.SharedSheet
            };

            return View(patientUpdateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, PatientUpdateModel patientUpdateModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(patientUpdateModel);
            }
            else
            {
                PatientModel patientModel = _patientService.GetPatientById(id);

                patientModel.SocialSecurityNumber = patientUpdateModel.SocialSecurityNumber;
                patientModel.LastName = patientUpdateModel.LastName;
                patientModel.FirstName = patientUpdateModel.FirstName;
                patientModel.BirthDate = patientUpdateModel.BirthDate;
                patientModel.BloodGroup = patientUpdateModel.BloodGroup;
                patientModel.Sex = patientUpdateModel.Sex;
                patientModel.SharedSheet = patientUpdateModel.SharedSheetWithOthersPractitioners;

                _patientService.UpdatePatient(patientModel);

                result = RedirectToAction(nameof(this.Index));
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            PatientDeleteModel patientDeleteModel = new()
            {
                Id = patientModel.Id,
                SocialSecurityNumber = SocialSecurityNumberHelper.AddBlankSpacesInSocialSecurityNumber(patientModel.SocialSecurityNumber),
                LastName = patientModel.LastName,
                FirstName = patientModel.FirstName,
                BirthDate = patientModel.BirthDate,
                BloodGroup = patientModel.BloodGroup,
                Sex = patientModel.Sex,
                SharedSheetWithOthersPractitioners = patientModel.SharedSheet
            };

            return View(patientDeleteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, PatientDeleteModel patientDeleteModel)
        {
            IActionResult result;

            PatientModel patientModel = _patientService.GetPatientById(id);

            _patientService.DeletePatient(patientModel);

            result = RedirectToAction(nameof(this.Index));

            return result;
        }

        #endregion CRUD Patients
    }
}