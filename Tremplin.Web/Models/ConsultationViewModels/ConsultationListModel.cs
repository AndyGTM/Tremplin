using System.ComponentModel;

namespace Tremplin.Models.ConsultationViewModels
{
    public class ConsultationListModel
    {
        [DisplayName("Date de consultation")]
        public DateTime Date { get; set; }

        [DisplayName("Description")]
        public string ShortDescription { get; set; }

        public int PatientId { get; set; }

        public List<ConsultationModel>? Consultations { get; set; }
    }
}