using Tremplin.Core.Enums;

namespace Tremplin.Data.Entity
{
    public class Patient : BaseEntity
    {
        public string SocialSecurityNumber { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime BirthDate { get; set; }

        public BloodGroupNames BloodGroup { get; set; }

        public SexTypes Sex { get; set; }

        public bool SharedSheet { get; set; }

        public string CreatedBy { get; set; }

        public List<Consultation> Consultations { get; set; }
    }
}