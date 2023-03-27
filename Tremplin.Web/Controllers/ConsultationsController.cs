using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data.Entity;
using Tremplin.IServices;
using Tremplin.Models;
using Tremplin.Models.ConsultationViewModels;

namespace Tremplin.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {
        private UserManager<User> UserManager { get; init; }

        private readonly IConsultationService _consultationService;

        private readonly IPatientService _patientService;

        public ConsultationsController(UserManager<User> aUserManager, IConsultationService consultationService, IPatientService patientService)
        {
            UserManager = aUserManager;
            _consultationService = consultationService;
            _patientService = patientService;
        }

        #region CRUD Consultations

        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Index(int id, ConsultationListModel consultationListModel)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            IEnumerable<ConsultationModel> consultationsModels = _consultationService.GetConsultations(id);

            consultationListModel.PatientId = id;
            consultationListModel.Consultations = consultationsModels.ToList();

            return View(consultationListModel);
        }

        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            ConsultationCreationModel consultationCreationModel = new()
            {
                Id = id,
                Date = DateTime.Today
            };

            return View(consultationCreationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationCreationModel consultationCreationModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(consultationCreationModel);
            }
            else
            {
                ConsultationModel consultationModel = new()
                {
                    Date = consultationCreationModel.Date,
                    ShortDescription = consultationCreationModel.ShortDescription,
                    LongDescription = consultationCreationModel.LongDescription,
                    PatientId = consultationCreationModel.Id
                };

                _consultationService.CreateConsultation(consultationModel);

                result = RedirectToAction(nameof(this.Index), new { id = consultationCreationModel.Id });
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, ConsultationDetailsModel consultationDetailsModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            consultationDetailsModel.Id = consultationModel.Id;
            consultationDetailsModel.Date = consultationModel.Date;
            consultationDetailsModel.ShortDescription = consultationModel.ShortDescription;
            consultationDetailsModel.LongDescription = consultationModel.LongDescription;
            consultationDetailsModel.PatientId = consultationModel.PatientId;

            return View(consultationDetailsModel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            ConsultationUpdateModel consultationUpdateModel = new()
            {
                Id = consultationModel.Id,
                Date = consultationModel.Date,
                ShortDescription = consultationModel.ShortDescription,
                LongDescription = consultationModel.LongDescription,
                PatientId = consultationModel.PatientId
            };

            return View(consultationUpdateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ConsultationUpdateModel consultationUpdateModel)
        {
            IActionResult result;

            if (!ModelState.IsValid)
            {
                result = View(consultationUpdateModel);
            }
            else
            {
                ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationUpdateModel.Id);

                consultationModel.Date = consultationUpdateModel.Date;
                consultationModel.ShortDescription = consultationUpdateModel.ShortDescription;
                consultationModel.LongDescription = consultationUpdateModel.LongDescription;

                _consultationService.UpdateConsultation(consultationModel);

                result = RedirectToAction(nameof(this.Index), new { id = consultationModel.PatientId });
            }

            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id, ConsultationDeleteModel consultationdDeleteModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            if (patientModel.CreatedBy != user.UserName)
            {
                return RedirectToAction("AccessDenied", "Users");
            }

            consultationdDeleteModel.Id = consultationModel.Id;
            consultationdDeleteModel.Date = consultationModel.Date;
            consultationdDeleteModel.ShortDescription = consultationModel.ShortDescription;
            consultationdDeleteModel.PatientId = consultationModel.PatientId;

            return View(consultationdDeleteModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ConsultationDeleteModel consultationdDeleteModel)
        {
            IActionResult result;

            ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationdDeleteModel.Id);

            consultationdDeleteModel.PatientId = consultationModel.PatientId;

            _consultationService.DeleteConsultation(consultationModel);

            result = RedirectToAction(nameof(this.Index), new { id = consultationdDeleteModel.PatientId });

            return result;
        }

        #endregion CRUD Consultations
    }
}