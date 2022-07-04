﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Tremplin.Core.Enums;

namespace Tremplin.Data.Entity.Patient
{
    [Table("Patient")]
    public class Patient : BaseEntity
    {
        /// <summary>
        /// Social security number
        /// </summary>
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// Patient last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Patient first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Patient birth date
        /// </summary>
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Patient blood group
        /// </summary>
        public BloodGroupNames BloodGroup { get; set; }

        /// <summary>
        /// Patient sex
        /// </summary>
        public SexTypes Sex { get; set; }

        /// <summary>
        /// Authorize or not the sharing of the patient sheet with others practitioners
        /// </summary>
        public bool SharedSheet { get; set; }

        /// <summary>
        /// User who created this patient
        /// </summary>
        public string CreatedBy { get; set; }

        public List<Consultation.Consultation> Consultations { get; set; }
    }
}