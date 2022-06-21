using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tremplin.Data;
using Tremplin.Models;

namespace Tremplin.Controllers
{
    [Authorize]
    public class ConsultationsController : Controller
    {
        private DataContext DataContext { get; init; }

        public ConsultationsController(DataContext dataContext)
        {
            DataContext = dataContext;
        }

        /// <summary>
        /// Provides access to the view for listing consultations
        /// </summary>
        /// <param name="id">Patient Id</param>
        [HttpGet]
        public async Task<IActionResult> Index(int id, ConsultationListViewModel consultationListViewModel)
        {
            IQueryable<Consultation> consultations = from m in DataContext.Consultations
                                                     where m.PatientId == id
                                                     select m;

            consultationListViewModel.PatientId = id;
            consultationListViewModel.Consultations = await consultations.ToListAsync();

            return View(consultationListViewModel);
        }

        /// <summary>
        /// Provides access to the view for creating a consultation
        /// </summary>
        /// <param name="id">Patient Id</param>
        /// <param name="consultationCreationViewModel">Consultation information</param>
        [HttpGet]
        public IActionResult Create(int id, ConsultationCreationViewModel consultationCreationViewModel)
        {
            consultationCreationViewModel.Id = id;

            return View(consultationCreationViewModel);
        }

        /// <summary>
        /// Allows to create a consultation
        /// </summary>
        /// <param name="consultationCreationViewModel">Consultation information</param>
        [HttpPost]
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
                Consultation consultation = new()
                {
                    Date = consultationCreationViewModel.Date,
                    ShortDescription = consultationCreationViewModel.ShortDescription,
                    LongDescription = consultationCreationViewModel.LongDescription,
                    PatientId = consultationCreationViewModel.Id
                };

                // Adding the consultation to the data context
                DataContext.Add(consultation);

                // Persistence of adding the consultation to the database
                await DataContext.SaveChangesAsync();

                result = this.RedirectToAction(nameof(this.Index), new { id = consultation.PatientId });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for updating a consultation
        /// </summary>
        [HttpGet]
        public IActionResult Update(int id, ConsultationUpdateViewModel consultationUpdateViewModel)
        {
            Consultation consultation = DataContext.Consultations.Find(id);

            consultationUpdateViewModel.Id = consultation.Id;

            consultationUpdateViewModel.Date = consultation.Date;
            consultationUpdateViewModel.ShortDescription = consultation.ShortDescription;
            consultationUpdateViewModel.LongDescription = consultation.LongDescription;
            consultationUpdateViewModel.PatientId = consultation.PatientId;

            return View(consultationUpdateViewModel);
        }

        /// <summary>
        /// Allows to update a consultation
        /// </summary>
        /// <param name="consultationUpdateViewModel">Consultation information</param>
        [HttpPost]
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
                Consultation consultation = DataContext.Consultations.Find(consultationUpdateViewModel.Id);

                consultation.Date = consultationUpdateViewModel.Date;
                consultation.ShortDescription = consultationUpdateViewModel.ShortDescription;
                consultation.LongDescription = consultationUpdateViewModel.LongDescription;

                // Updating the consultation to the data context
                DataContext.Consultations.Update(consultation);

                // Persistence of updating the consultation to the database
                await DataContext.SaveChangesAsync();

                result = this.RedirectToAction(nameof(this.Index), new { id = consultation.PatientId });
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for deleting a consultation
        /// </summary>
        [HttpGet]
        public IActionResult Delete(int id, ConsultationDeleteViewModel consultationdDeleteViewModel)
        {
            Consultation consultation = DataContext.Consultations.Find(id);

            consultationdDeleteViewModel.Id = consultation.Id;
            
            consultationdDeleteViewModel.Date = consultation.Date;
            consultationdDeleteViewModel.ShortDescription = consultation.ShortDescription;
            consultationdDeleteViewModel.LongDescription = consultation.LongDescription;
            consultationdDeleteViewModel.PatientId = consultation.PatientId;

            return View(consultationdDeleteViewModel);
        }

        /// <summary>
        /// Allows to delete a consultation
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Delete(ConsultationDeleteViewModel consultationdDeleteViewModel)
        {
            IActionResult result;

            // Consultation delete
            Consultation consultation = DataContext.Consultations.Find(consultationdDeleteViewModel.Id);

            consultationdDeleteViewModel.PatientId = consultation.PatientId;

            // Deleting the consultation to the data context
            DataContext.Consultations.Remove(consultation);

            // Persistence of deleting the consultation to the database
            await DataContext.SaveChangesAsync();

            result = this.RedirectToAction(nameof(this.Index), new { id = consultationdDeleteViewModel.PatientId });

            return result;
        }
    }
}