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
        [HttpGet]
        public async Task<IActionResult> Index(int patientId)
        {
            IQueryable<Consultation> consultations = from m in DataContext.Consultations
                                                     where m.PatientId == patientId
                                                     select m;

            ConsultationListViewModel consultationListViewModel = new ConsultationListViewModel
            {
                Consultations = await consultations.ToListAsync()
            };

            return View(consultationListViewModel);
        }
    }
}