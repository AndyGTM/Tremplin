namespace Tremplin.Data.Entity
{
    public class Consultation : BaseEntity
    {
        public DateTime Date { get; set; }

        public string ShortDescription { get; set; }

        public string? LongDescription { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }
    }
}