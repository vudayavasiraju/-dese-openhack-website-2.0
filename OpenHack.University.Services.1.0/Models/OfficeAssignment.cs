using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OpenHack.University.Services.Models
{
    public class OfficeAssignment
    {
        [Key]
        public int InstructorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }

        public Instructor Instructor { get; set; }

        public Contract.OfficeAssignment GetContract()
        {
            var contractOfficeAssignment = new Contract.OfficeAssignment()
            {
                InstructorID = InstructorID,
                Location = Location
            };

            return contractOfficeAssignment;
        }
    }
}