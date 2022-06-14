﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tremplin.Data;
using Tremplin.Models;

namespace Tremplin.Controllers
{
    [Authorize]
    public class PatientsController : Controller
    {
        private DataContext DataContext { get; init; }

        public PatientsController(DataContext dataContext)
        {
            DataContext = dataContext;
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
        /// <param name="patientCreationViewModel">User information</param>
        [HttpPost]
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
                // Patient creation
                Patient patient = new()
                {
                    SocialSecurityNumber = patientCreationViewModel.SocialSecurityNumber,
                    LastName = patientCreationViewModel.LastName,
                    FirstName = patientCreationViewModel.FirstName,
                    BirthDate = patientCreationViewModel.BirthDate,
                    BloodGroup = patientCreationViewModel.BloodGroup,
                    Sex = patientCreationViewModel.Sex,
                    SharedSheet = patientCreationViewModel.SharedSheet
                };

                // Adding the patient to the data context
                DataContext.Add(patient);

                // Persistence of adding the patient to the database
                await DataContext.SaveChangesAsync();

                result = this.RedirectToAction(nameof(Index), "Home");
            }

            return result;
        }
    }
}