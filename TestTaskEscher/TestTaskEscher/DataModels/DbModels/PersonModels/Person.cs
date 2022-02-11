using System.ComponentModel.DataAnnotations;
using TestTaskEscher.DataModel;

namespace TestTaskEscher.DataModels.DbModels.PersonModels
{
    public class Person : BasePerson
    {
        [Required]
        public DateTime DateOfRegistration { get; set; }

        [Required]
        public bool RegistrationAllowed { get; set; }

        public int SpouseId { get; set; }
        public Spouse Spouse { get; set; }

        public override string ToString()
        {
            var single = "Single";
            var married = "Married";

            return base.ToString()
                   + $"|{RegistrationAllowed}"
                   + $"|{(SpouseId == 0 ? single : married)}";
        }
    }
}