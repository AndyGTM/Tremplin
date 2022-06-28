﻿using Tremplin.Data;

namespace Tremplin.IServices.IConsultation
{
    public interface IConsultationService
    {
        #region CRUD Consultations

        /// <summary>
        /// Gets list of consultations by patient Id
        /// </summary>
        IQueryable<Consultation> GetConsultations(int idPatient);

        /// <summary>
        /// Creation of a consultation for the selected patient
        /// </summary>
        void CreateConsultation(DateTime date, string shortDescription, string? longDescription, int patientId);

        /// <summary>
        /// Update of a consultation for the selected patient
        /// </summary>
        void UpdateConsultation(Consultation consultation, DateTime date, string shortDescription, string? longDescription);
        
        #endregion CRUD Consultations
    }
}