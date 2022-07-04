using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data.Entity.User;
using Tremplin.IServices.IConsultation;
using Tremplin.IServices.IPatient;
using Tremplin.Models.Consultation;
using Tremplin.Models.ConsultationViewModels;
using Tremplin.Models.Patient;

namespace Tremplin.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {
        /// <summary>
        /// User manager
        /// </summary>
        private UserManager<User> UserManager { get; init; }

        private readonly IConsultationService _consultationService;

        private readonly IPatientService _patientService;

        public ConsultationsController(UserManager<User> aUserManager, IConsultationService consultationService, IPatientService patientService)
        {
            this.UserManager = aUserManager;
            _consultationService = consultationService;
            _patientService = patientService;
        }

        #region CRUD Consultations

        /// <summary>
        /// Provides access to the view for listing consultations
        /// </summary>
        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Index(int id, ConsultationListModel consultationListModel)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            IEnumerable<ConsultationModel> consultationsModels = _consultationService.GetConsultations(id);

            consultationListModel.PatientId = id;
            consultationListModel.Consultations = consultationsModels.ToList();

            return View(consultationListModel);
        }

        /// <summary>
        /// Provides access to the view for creating a consultation
        /// </summary>
        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            ConsultationCreationModel consultationCreationModel = new()
            {
                Id = id,
                Date = DateTime.Today
            };

            return View(consultationCreationModel);
        }

        /// <summary>
        /// Allows to create a consultation
        /// </summary>
        /// <param name="consultationCreationModel">Consultation information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationCreationModel consultationCreationModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(consultationCreationModel);
            }
            else
            {
                // Consultation creation
                _consultationService.CreateConsultation
                    (
                        consultationCreationModel.Date,
                        consultationCreationModel.ShortDescription,
                        consultationCreationModel.LongDescription,
                        consultationCreationModel.Id
                    );

                result = this.RedirectToAction(nameof(this.Index), new { id = consultationCreationModel.Id });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for consultation's details
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id, ConsultationDetailsModel consultationDetailsModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationDetailsModel.Id = consultationModel.Id;

            consultationDetailsModel.Date = consultationModel.Date;
            consultationDetailsModel.ShortDescription = consultationModel.ShortDescription;
            consultationDetailsModel.LongDescription = consultationModel.LongDescription;
            consultationDetailsModel.PatientId = consultationModel.PatientId;

            return View(consultationDetailsModel);
        }

        /// <summary>
        /// Provides access to the view for updating a consultation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
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

        /// <summary>
        /// Allows to update a consultation
        /// </summary>
        /// <param name="consultationUpdateModel">Consultation information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ConsultationUpdateModel consultationUpdateModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(consultationUpdateModel);
            }
            else
            {
                // Consultation update
                ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationUpdateModel.Id);

                _consultationService.UpdateConsultation
                    (
                        consultationModel,
                        consultationUpdateModel.Date,
                        consultationUpdateModel.ShortDescription,
                        consultationUpdateModel.LongDescription
                    );

                result = this.RedirectToAction(nameof(this.Index), new { id = consultationModel.PatientId });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for deleting a consultation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int id, ConsultationDeleteModel consultationdDeleteModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationdDeleteModel.Id = consultationModel.Id;
            consultationdDeleteModel.Date = consultationModel.Date;
            consultationdDeleteModel.ShortDescription = consultationModel.ShortDescription;
            consultationdDeleteModel.PatientId = consultationModel.PatientId;

            return View(consultationdDeleteModel);
        }

        /// <summary>
        /// Allows to delete a consultation
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ConsultationDeleteModel consultationdDeleteModel)
        {
            IActionResult result;

            // Consultation delete
            ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationdDeleteModel.Id);

            consultationdDeleteModel.PatientId = consultationModel.PatientId;

            _consultationService.DeleteConsultation(consultationModel);

            result = this.RedirectToAction(nameof(this.Index), new { id = consultationdDeleteModel.PatientId });

            return result;
        }

        #endregion CRUD Consultations
    }
}