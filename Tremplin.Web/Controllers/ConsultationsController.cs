using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.IServices.IConsultation;
using Tremplin.IServices.IPatient;
using Tremplin.Models.ConsultationViewModels;

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

            Patient patient = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patient.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            IQueryable<Consultation> consultations = _consultationService.GetConsultations(id);

            consultationListViewModel.PatientId = id;
            consultationListViewModel.Consultations = await consultations.ToListAsync();

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

            Patient patient = _patientService.GetPatientById(id);

            // Check if user has the rights to access this view
            if (patient.CreatedBy != user.UserName)
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

            Consultation consultation = _consultationService.GetConsultationById(id);

            Patient patient = _patientService.GetPatientById(consultation.PatientId);

            // Check if user has the rights to access this view
            if (patient.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationDetailsViewModel.Id = consultation.Id;

            consultationDetailsViewModel.Date = consultation.Date;
            consultationDetailsViewModel.ShortDescription = consultation.ShortDescription;
            consultationDetailsViewModel.LongDescription = consultation.LongDescription;
            consultationDetailsViewModel.PatientId = consultation.PatientId;

            return View(consultationDetailsViewModel);
        }

        /// <summary>
        /// Provides access to the view for updating a consultation
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            User user = await UserManager.GetUserAsync(User);

            Consultation consultation = _consultationService.GetConsultationById(id);

            Patient patient = _patientService.GetPatientById(consultation.PatientId);

            // Check if user has the rights to access this view
            if (patient.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            ConsultationUpdateViewModel consultationUpdateViewModel = new()
            {
                Id = consultation.Id,
                Date = consultation.Date,
                ShortDescription = consultation.ShortDescription,
                LongDescription = consultation.LongDescription,
                PatientId = consultation.PatientId
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
                Consultation consultation = _consultationService.GetConsultationById(consultationUpdateViewModel.Id);

                _consultationService.UpdateConsultation
                    (
                        consultation,
                        consultationUpdateViewModel.Date,
                        consultationUpdateViewModel.ShortDescription,
                        consultationUpdateViewModel.LongDescription
                    );

                result = this.RedirectToAction(nameof(this.Index), new { id = consultation.PatientId });
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

            Consultation consultation = _consultationService.GetConsultationById(id);

            Patient patient = _patientService.GetPatientById(consultation.PatientId);

            // Check if user has the rights to access this view
            if (patient.CreatedBy != user.UserName)
            {
                return this.RedirectToAction("AccessDenied", "Users");
            }

            consultationdDeleteViewModel.Id = consultation.Id;
            consultationdDeleteViewModel.Date = consultation.Date;
            consultationdDeleteViewModel.ShortDescription = consultation.ShortDescription;
            consultationdDeleteViewModel.PatientId = consultation.PatientId;

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
            Consultation consultation = _consultationService.GetConsultationById(consultationdDeleteViewModel.Id);

            consultationdDeleteViewModel.PatientId = consultation.PatientId;

            _consultationService.DeleteConsultation(consultation);

            result = this.RedirectToAction(nameof(this.Index), new { id = consultationdDeleteViewModel.PatientId });

            return result;
        }
    }
}