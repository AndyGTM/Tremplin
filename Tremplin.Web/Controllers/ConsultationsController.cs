using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data;
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

        /// <summary>
        /// Provides access to the view for listing consultations
        /// </summary>
        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Index(int id, ConsultationListViewModel consultationListViewModel)
        {
            User user = await UserManager.GetUserAsync(User);

            PatientModel patientModel = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            IEnumerable<ConsultationModel> consultationsModels = _consultationService.GetConsultations(id);

            consultationListViewModel.PatientId = id;
            consultationListViewModel.Consultations = consultationsModels.ToList();

            return View(consultationListViewModel);
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

            ConsultationCreationViewModel consultationCreationViewModel = new()
            {
                Id = id,
                Date = DateTime.Today
            };

            return View(consultationCreationViewModel);
        }

        /// <summary>
        /// Allows to create a consultation
        /// </summary>
        /// <param name="consultationCreationViewModel">Consultation information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationCreationViewModel consultationCreationViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(consultationCreationViewModel);
            }
            else
            {
                // Consultation creation
                _consultationService.CreateConsultation
                    (
                        consultationCreationViewModel.Date,
                        consultationCreationViewModel.ShortDescription,
                        consultationCreationViewModel.LongDescription,
                        consultationCreationViewModel.Id
                    );

                result = this.RedirectToAction(nameof(this.Index), new { id = consultationCreationViewModel.Id });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for consultation's details
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id, ConsultationDetailsViewModel consultationDetailsViewModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationDetailsViewModel.Id = consultationModel.Id;

            consultationDetailsViewModel.Date = consultationModel.Date;
            consultationDetailsViewModel.ShortDescription = consultationModel.ShortDescription;
            consultationDetailsViewModel.LongDescription = consultationModel.LongDescription;
            consultationDetailsViewModel.PatientId = consultationModel.PatientId;

            return View(consultationDetailsViewModel);
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

            ConsultationUpdateViewModel consultationUpdateViewModel = new()
            {
                Id = consultationModel.Id,
                Date = consultationModel.Date,
                ShortDescription = consultationModel.ShortDescription,
                LongDescription = consultationModel.LongDescription,
                PatientId = consultationModel.PatientId
            };

            return View(consultationUpdateViewModel);
        }

        /// <summary>
        /// Allows to update a consultation
        /// </summary>
        /// <param name="consultationUpdateViewModel">Consultation information</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ConsultationUpdateViewModel consultationUpdateViewModel)
        {
            IActionResult result;

            // Valid input data ?
            if (!this.ModelState.IsValid)
            {
                result = this.View(consultationUpdateViewModel);
            }
            else
            {
                // Consultation update
                ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationUpdateViewModel.Id);

                _consultationService.UpdateConsultation
                    (
                        consultationModel,
                        consultationUpdateViewModel.Date,
                        consultationUpdateViewModel.ShortDescription,
                        consultationUpdateViewModel.LongDescription
                    );

                result = this.RedirectToAction(nameof(this.Index), new { id = consultationModel.PatientId });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for deleting a consultation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Delete(int id, ConsultationDeleteViewModel consultationdDeleteViewModel)
        {
            User user = await UserManager.GetUserAsync(User);

            ConsultationModel consultationModel = _consultationService.GetConsultationById(id);

            PatientModel patientModel = _patientService.GetPatientById(consultationModel.PatientId);

            // Check if user has the rights to access this view
            if (patientModel.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationdDeleteViewModel.Id = consultationModel.Id;
            consultationdDeleteViewModel.Date = consultationModel.Date;
            consultationdDeleteViewModel.ShortDescription = consultationModel.ShortDescription;
            consultationdDeleteViewModel.PatientId = consultationModel.PatientId;

            return View(consultationdDeleteViewModel);
        }

        /// <summary>
        /// Allows to delete a consultation
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ConsultationDeleteViewModel consultationdDeleteViewModel)
        {
            IActionResult result;

            // Consultation delete
            ConsultationModel consultationModel = _consultationService.GetConsultationById(consultationdDeleteViewModel.Id);

            consultationdDeleteViewModel.PatientId = consultationModel.PatientId;

            _consultationService.DeleteConsultation(consultationModel);

            result = this.RedirectToAction(nameof(this.Index), new { id = consultationdDeleteViewModel.PatientId });

            return result;
        }
    }
}