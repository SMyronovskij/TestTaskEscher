using System;
using System.ComponentModel.DataAnnotations;

namespace TestTaskEscher.DataModels.DbModels.PersonModels
{
    public class BasePerson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        public override string ToString()
        {
            return $"{Id}" +
                   $"|{FirstName}" +
                   $"|{Surname}" +
                   $"|{DateOfBirth:d}";
        }
    }
}
