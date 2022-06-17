﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
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
        /// Provides access to the view for listing patients
        /// </summary>
        [HttpGet]
        public IActionResult Index(PatientListViewModel patientListViewModel)
        {
            IEnumerable<Patient> patientsDB = DataContext.Patients;

            patientListViewModel.Patients = patientsDB.ToList();

            foreach (Patient patient in patientListViewModel.Patients)
            {
                // Adding blank spaces for displaying the social security number
                patient.SocialSecurityNumber = Regex.Replace(patient.SocialSecurityNumber, @"(\w{1})(\w{2})(\w{2})(\w{2})(\w{3})(\w{3})(\w{2})", @"$1 $2 $3 $4 $5 $6 $7");
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
                    // Removal of any blank spaces for recording the social security number in the database
                    SocialSecurityNumber = Regex.Replace(patientCreationViewModel.SocialSecurityNumber, @"\s", ""),

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

                result = this.RedirectToAction(nameof(this.Index));
            }

            return result;
        }

        /// <summary>
        /// Provides access to the view for updating a patient
        /// </summary>
        [HttpGet]
        public IActionResult Update(int id)
        {
            Patient patientDB = DataContext.Patients.Find(id);

            PatientUpdateViewModel patientUpdateViewModel = new PatientUpdateViewModel();

            patientUpdateViewModel.Id = patientDB.Id;

            // Adding blank spaces for displaying the social security number
            patientUpdateViewModel.SocialSecurityNumber = Regex.Replace(patientDB.SocialSecurityNumber, @"(\w{1})(\w{2})(\w{2})(\w{2})(\w{3})(\w{3})(\w{2})", @"$1 $2 $3 $4 $5 $6 $7");

            patientUpdateViewModel.LastName = patientDB.LastName;
            patientUpdateViewModel.FirstName = patientDB.FirstName;
            patientUpdateViewModel.BirthDate = patientDB.BirthDate;
            patientUpdateViewModel.BloodGroup = patientDB.BloodGroup;
            patientUpdateViewModel.Sex = patientDB.Sex;
            patientUpdateViewModel.SharedSheet = patientDB.SharedSheet;

            return View(patientUpdateViewModel);
        }

        /// <summary>
        /// Allows to update a patient
        /// </summary>
        /// <param name="patientUpdateViewModel">Patient information</param>
        [HttpPost]
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
                Patient patient = DataContext.Patients.Find(id);

                // Removal of any blank spaces for recording the social security number in the database
                patient.SocialSecurityNumber = Regex.Replace(patientUpdateViewModel.SocialSecurityNumber, @"\s", "");

                patient.LastName = patientUpdateViewModel.LastName;
                patient.FirstName = patientUpdateViewModel.FirstName;
                patient.BirthDate = patientUpdateViewModel.BirthDate;
                patient.BloodGroup = patientUpdateViewModel.BloodGroup;
                patient.Sex = patientUpdateViewModel.Sex;
                patient.SharedSheet = patientUpdateViewModel.SharedSheet;
               
                // Updating the patient to the data context
                DataContext.Update(patient);

                // Persistence of updating the patient to the database
                await DataContext.SaveChangesAsync();

                result = this.RedirectToAction(nameof(this.Index));
            }

            return result;
        }
    }
}